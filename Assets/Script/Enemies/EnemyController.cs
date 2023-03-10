using UnityEngine;
using Pathfinding;
using System.Collections;
using Assets.Script.Player;

[RequireComponent(typeof(EnemyInterfaceObject))]
[RequireComponent(typeof(EnemyMoving))]
[RequireComponent(typeof(EnemyCharacter))]
[RequireComponent(typeof(EnemyAudio))]
[RequireComponent(typeof(Seeker))]
public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyVisual _enemyVisual;
    [SerializeField] private EnemyAttackTriggerZonePlayerDetector _enemyAttackTriggerZonePlayerDetector;
    [SerializeField] private EnemyAttackDamageZonePlayerDetector _enemyAttackDamageZonePlayerDetector;
    [SerializeField] private float _nextWaypointDistance;
    [SerializeField] private float _pathUpdateIntervalSeconds;
    [SerializeField] private float _attackCooldownSeconds;
    [SerializeField] private float _secondsBeforeDestroy;
    public EnemyInterfaceObject InterfaceObject => _enemyInterfaceObject;
    private Transform _targetTransform;
    private EnemyInterfaceObject _enemyInterfaceObject;
    private EnemyMoving _enemyMoving;
    private EnemyCharacter _enemyCharacter;
    private EnemyAudio _enemyAudio;
    private Seeker _seeker;
    private Path _currentPath;
    private int _currentWaypointIndex;
    private bool _isAttackNotCooldowning = true;
    private bool _wasInitialized = false;
    private bool _isAllActionsEnabled = true;
    private bool _isQuitting = false;
    protected abstract void RegistrateAction();
    protected abstract void RemoveRegistratationAction();
    protected virtual void SpecialAttack(PlayerController player)
    {
        _enemyCharacter.DamagePlayer(player);
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting)
        {
            RemoveRegistratationAction();
        }
    }

    private void Awake()
    {
        _enemyInterfaceObject = GetComponent<EnemyInterfaceObject>();
        _enemyMoving = GetComponent<EnemyMoving>();
        _enemyCharacter = GetComponent<EnemyCharacter>();
        _enemyAudio = GetComponent<EnemyAudio>();
        _seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        RegistrateAction();
        _enemyMoving.EnableMoving();
        InvokeRepeating(nameof(UpdatePathToPlayer), 0f, _pathUpdateIntervalSeconds);
    }

    private void OnEnable()
    {
        if (!_wasInitialized)
        {
            _enemyInterfaceObject.Initialize += OnInitialize;
        }
        _enemyInterfaceObject.Initialize += OnInitialize;
        _enemyInterfaceObject.AcceptedDieRequest += OnAcceptedDieRequest;
        _enemyVisual.AttackAnimationHitMomentStart += OnAttackAnimationHitMomentStart;
        _enemyVisual.MoveAnimationStepMoment += OnMoveAnimationStepMoment;
        _enemyInterfaceObject.AcceptedDisableMovingRequest += OnAcceptedDisableMovingRequest;
        _enemyInterfaceObject.AcceptedEnableMovingRequest += OnAcceptedEnableMovingRequest;
        _enemyInterfaceObject.AcceptedDisableAllActionsRequest += OnAcceptedDisableAllActionsRequest;
    }

    private void OnDisable()
    {
        if (!_wasInitialized)
        {
            _enemyInterfaceObject.Initialize -= OnInitialize;
        }
        _enemyInterfaceObject.AcceptedDieRequest -= OnAcceptedDieRequest;
        _enemyVisual.AttackAnimationHitMomentStart -= OnAttackAnimationHitMomentStart;
        _enemyVisual.MoveAnimationStepMoment -= OnMoveAnimationStepMoment;
        _enemyInterfaceObject.AcceptedDisableMovingRequest -= OnAcceptedDisableMovingRequest;
        _enemyInterfaceObject.AcceptedEnableMovingRequest -= OnAcceptedEnableMovingRequest;
        _enemyInterfaceObject.AcceptedDisableAllActionsRequest -= OnAcceptedDisableAllActionsRequest;
    }

    private void OnInitialize(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        _wasInitialized = true;
        _enemyInterfaceObject.Initialize -= OnInitialize;
    }

    private void Update()
    {
        if (_isAttackNotCooldowning && _enemyAttackTriggerZonePlayerDetector.IsPlayerInsideTriggerZone && _enemyCharacter.IsAlive && _isAllActionsEnabled)
        {
            _isAttackNotCooldowning = false;
            _enemyVisual.StartAttackAnimation();
            StartCoroutine(AttackCooldownHandler());
        }
    }

    private void FixedUpdate()
    {
        if (_currentPath == null || !_enemyCharacter.IsAlive || !_isAllActionsEnabled)
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
        if (_enemyCharacter.IsAlive && _seeker.IsDone())
        {
            _seeker.StartPath(transform.position, _targetTransform.position, OnPathComplete);
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
        _isAttackNotCooldowning = true;
    }

    private void OnAttackAnimationHitMomentStart()
    {
        _enemyAudio.PlayAttackSound();
        if (_enemyAttackDamageZonePlayerDetector.IsPlayerInside)
        {
            SpecialAttack(_enemyAttackDamageZonePlayerDetector.Player);
        }
    }

    private void OnMoveAnimationStepMoment()
    {
        _enemyAudio.PlayStepSound();
    }

    private void OnAcceptedDisableAllActionsRequest()
    {
        _enemyVisual.DisableMoving();
        _enemyMoving.DisableMoving();
        _isAllActionsEnabled = false;
    }

    private void OnAcceptedDisableMovingRequest()
    {
        _enemyVisual.DisableMoving();
        _enemyMoving.DisableMoving();
    }

    private void OnAcceptedEnableMovingRequest()
    {
        _enemyVisual.EnableMoving();
        _enemyMoving.EnableMoving();
    }

    private void OnAcceptedDieRequest()
    {
        _enemyMoving.ProcessDying();
        _enemyAudio.PlayDieSound();
        _enemyVisual.StartDieAnimation();
        _enemyCharacter.Die();
        Invoke(nameof(Destroy), _secondsBeforeDestroy);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}
