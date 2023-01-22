using UnityEngine;

[RequireComponent(typeof(AttackZoneEnemiesDetector))]
[RequireComponent(typeof(AttackZoneVisual))]
[RequireComponent(typeof(AttackZoneMechanics))]
public class AttackZoneController : MonoBehaviour
{
    private AttackZoneEnemiesDetector _attackZoneEnemiesDetector;
    private AttackZoneVisual _attackZoneVisual;
    private AttackZoneMechanics _attackZoneMechanics;

    public void AttackEnemies()
    {
        _attackZoneVisual.StartAttackAnimation();
    }

    private void Awake()
    {
        _attackZoneEnemiesDetector = GetComponent<AttackZoneEnemiesDetector>();
        _attackZoneVisual = GetComponent<AttackZoneVisual>();
        _attackZoneMechanics = GetComponent<AttackZoneMechanics>();
    }

    private void OnEnable()
    {
        _attackZoneVisual.AttackAnimationHitMomentStart += OnAttackAnimationHitMomentStart;
    }

    private void OnDisable()
    {
        _attackZoneVisual.AttackAnimationHitMomentStart -= OnAttackAnimationHitMomentStart;
    }

    private void OnAttackAnimationHitMomentStart()
    {
        _attackZoneMechanics.DamageEnemies(_attackZoneEnemiesDetector.EnemiesInAttackZone);
        _attackZoneEnemiesDetector.ClearEnemiesInAttackZoneList();
    }

}
