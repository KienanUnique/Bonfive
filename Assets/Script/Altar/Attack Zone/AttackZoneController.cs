using UnityEngine;

[RequireComponent(typeof(AttackZoneEnemiesDetector))]
[RequireComponent(typeof(AttackZoneVisual))]
[RequireComponent(typeof(AttackZoneMechanics))]
[RequireComponent(typeof(AttackZoneAudio))]
public class AttackZoneController : MonoBehaviour
{
    private AttackZoneEnemiesDetector _attackZoneEnemiesDetector;
    private AttackZoneVisual _attackZoneVisual;
    private AttackZoneMechanics _attackZoneMechanics;
    private AttackZoneAudio _attackZoneAudio;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _attackZoneEnemiesDetector.SetupSteps(stepsCount, startStep);
    }

    public void IncreaseAttackZone()
    {
        _attackZoneEnemiesDetector.IncreaseDetectionRadius();
    }

    public void DecreaseAttackZone()
    {
        _attackZoneEnemiesDetector.DecreaseDetectionRadius();

    }

    public void AttackEnemies()
    {
        _attackZoneVisual.StartAttackAnimation();
    }

    private void Awake()
    {
        _attackZoneEnemiesDetector = GetComponent<AttackZoneEnemiesDetector>();
        _attackZoneVisual = GetComponent<AttackZoneVisual>();
        _attackZoneMechanics = GetComponent<AttackZoneMechanics>();
        _attackZoneAudio = GetComponent<AttackZoneAudio>();
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
        _attackZoneAudio.PlayAttackSound();
        _attackZoneMechanics.DamageEnemies(_attackZoneEnemiesDetector.EnemiesInAttackZone);
        _attackZoneEnemiesDetector.ClearEnemiesInAttackZoneList();
    }

}
