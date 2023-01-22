using UnityEngine;
using Assets.Script.Player;

[RequireComponent(typeof(SafeZoneMechanics))]
[RequireComponent(typeof(SafeZoneCharactersDetector))]
[RequireComponent(typeof(SafeZonePhysics))]
public class SafeZoneController : MonoBehaviour
{
    private SafeZoneMechanics _safeZoneMechanics;
    private SafeZoneCharactersDetector _safeZonePlayerDetector;
    private SafeZonePhysics _safeZonePhysics;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _safeZonePlayerDetector.SetupSteps(stepsCount, startStep);
        _safeZonePhysics.SetupSteps(stepsCount, startStep);
    }
    public void IncreaseSafeZone()
    {
        _safeZonePlayerDetector.IncreaseDetectionRadius();
        _safeZonePhysics.IncreaseForceMagnitude();
    }

    public void DecreaseSafeZone()
    {
        _safeZonePlayerDetector.DecreaseDetectionRadius();
        _safeZonePhysics.DecreaseForceMagnitude();
    }
    private void Awake()
    {
        _safeZoneMechanics = GetComponent<SafeZoneMechanics>();
        _safeZonePlayerDetector = GetComponent<SafeZoneCharactersDetector>();
        _safeZonePhysics = GetComponent<SafeZonePhysics>();
    }

    private void OnEnable()
    {
        _safeZonePlayerDetector.PlayerEnter += OnPlayerEnter;
        _safeZonePlayerDetector.PlayerExit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _safeZonePlayerDetector.PlayerEnter -= OnPlayerEnter;
        _safeZonePlayerDetector.PlayerExit -= OnPlayerExit;
    }

    private void OnPlayerEnter(PlayerController enteredPlayerController)
    {
        _safeZoneMechanics.StartHealPlayerCharacter(enteredPlayerController);
    }

    private void OnPlayerExit()
    {
        _safeZoneMechanics.StopHealPlayerCharacter();
    }

    private void OnEnemyEnter(EnemyController enteredEnemy)
    {
        enteredEnemy.DisableMoving();
    }

    private void OnEnemyExit(EnemyController exitingEnemy)
    {
        exitingEnemy.EnableMoving();
    }
}
