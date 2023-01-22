using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyMoving))]
[RequireComponent(typeof(EnemyCharacter))]
[RequireComponent(typeof(Seeker))]
public class SkeletonController : MonoBehaviour
{
    [SerializeField] private EnemyVisual _enemyVisual;
    [SerializeField] private EnemyAttackTriggerZonePlayerDetector _enemyAttackTriggerZonePlayerDetector;
    [SerializeField] private EnemyAttackDamageZonePlayerDetector _enemyAttackDamageZonePlayerDetector;
    [SerializeField] private float _nextWaypointDistance;
    [SerializeField] private float _pathUpdateIntervalSeconds;
    [SerializeField] private float _attackCooldownSeconds;
    private EnemyController _enemyController;
    private EnemyMoving _enemyMoving;
    private EnemyCharacter _enemyCharacter;
    private Seeker _seeker;
    private Path _currentPath;
    private int _currentWaypointIndex;
    private bool _canAttack = true;
    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _enemyMoving = GetComponent<EnemyMoving>();
        _enemyCharacter = GetComponent<EnemyCharacter>();
        _seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        _enemyMoving.EnableMoving();
        InvokeRepeating(nameof(UpdatePathToPlayer), 0f, _pathUpdateIntervalSeconds);
    }

    private void OnEnable()
    {
        _enemyVisual.AttackAnimationHitMomentStart += OnAttackAnimationHitMomentStart;
        _enemyController.AcceptedDisableMovingRequest += OnAcceptedDisableMovingRequest;
        _enemyController.AcceptedEnableMovingRequest += OnAcceptedEnableMovingRequest;
    }

    private void OnDisable()
    {
        _enemyVisual.AttackAnimationHitMomentStart -= OnAttackAnimationHitMomentStart;
        _enemyController.AcceptedDisableMovingRequest -= OnAcceptedDisableMovingRequest;
        _enemyController.AcceptedEnableMovingRequest -= OnAcceptedEnableMovingRequest;
    }

    private void Update()
    {
        if (_canAttack && _enemyAttackTriggerZonePlayerDetector.IsPlayerInsideTriggerZone)
        {
            _canAttack = false;
            _enemyVisual.StartAttackAnimation();
            StartCoroutine(AttackCooldownHandler());
        }
    }

    private void FixedUpdate()
    {
        if (_currentPath == null)
        {
            return;
        }

        TryUpdateCurrentWaypoint();

        if (_currentWaypointIndex >= _currentPath.vectorPath.Count)
        {
            _enemyMoving.StopMoving();
            _enemyVisual.StartIdleAnimation();
        }
        else
        {
            var moveDirection = (_currentPath.vectorPath[_currentWaypointIndex] - transform.position);
            moveDirection.Normalize();
            _enemyMoving.Move(moveDirection);
            _enemyVisual.StartMovingAnimation(moveDirection);
        }
    }

    private void UpdatePathToPlayer()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(transform.position, GameController.Instance.Player.CurrentPosition, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _currentPath = path;
            _currentWaypointIndex = 0;
        }
    }

    private void TryUpdateCurrentWaypoint()
    {
        if (_currentWaypointIndex < _currentPath.vectorPath.Count
            && Vector2.Distance(_currentPath.vectorPath[_currentWaypointIndex], transform.position) <= _nextWaypointDistance)
        {
            _currentWaypointIndex++;
        }
    }

    private IEnumerator AttackCooldownHandler()
    {
        yield return new WaitForSeconds(_attackCooldownSeconds);
        _canAttack = true;
    }

    public void OnAttackAnimationHitMomentStart()
    {
        if (_enemyAttackDamageZonePlayerDetector.IsPlayerInside)
        {
            _enemyCharacter.DamagePlayer(_enemyAttackDamageZonePlayerDetector.Player);
        }
    }

    private void OnAcceptedDisableMovingRequest(){
        _enemyVisual.DisableMoving();
        _enemyMoving.DisableMoving();
    }

    private void OnAcceptedEnableMovingRequest(){
        _enemyVisual.EnableMoving();
        _enemyMoving.EnableMoving();
    }

}