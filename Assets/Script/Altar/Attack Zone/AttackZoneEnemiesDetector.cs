using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AttackZoneEnemiesDetector : MonoBehaviour, IStepsControllable
{
    [SerializeField] private float _minimumDetectionRadius;
    [SerializeField] private float _maximumDetectionRadius;
    public List<EnemyController> EnemiesInAttackZone { get; private set; } = new List<EnemyController>();
    private CircleCollider2D _attackZoneEnemies;
    private float _detectionRadiusStep;
    public void SetupSteps(int stepsCount, int startStep)
    {
        _detectionRadiusStep = (_maximumDetectionRadius - _minimumDetectionRadius) / stepsCount;
        ApplyNewStep(startStep);
    }
    public void ApplyNewStep(int newStep)
    {
        _attackZoneEnemies.radius = _minimumDetectionRadius + _detectionRadiusStep * newStep;
    }

    public void ClearEnemiesInAttackZoneList()
    {
        EnemiesInAttackZone.Clear();
    }

    private void Awake()
    {
        _attackZoneEnemies = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && !EnemiesInAttackZone.Contains(enemyController))
        {
            EnemiesInAttackZone.Add(enemyController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && EnemiesInAttackZone.Contains(enemyController))
        {
            EnemiesInAttackZone.Remove(enemyController);
        }
    }
}
