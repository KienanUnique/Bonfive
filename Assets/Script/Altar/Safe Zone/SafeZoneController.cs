using UnityEngine;
using Assets.Script.Player;

[RequireComponent(typeof(SafeZoneMechanics))]
[RequireComponent(typeof(SafeZonePlayerDetector))]
public class SafeZoneController : MonoBehaviour
{
    private SafeZoneMechanics _safeZoneMechanics;
    private SafeZonePlayerDetector _safeZonePlayerDetector;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _safeZonePlayerDetector.SetupSteps(stepsCount, startStep);
    }
    public void IncreaseSafeZone()
    {
        _safeZonePlayerDetector.IncreasePlayerDetectionRadius();
    }

    public void DecreaseSafeZone()
    {
        _safeZonePlayerDetector.DecreasePlayerDetectionRadius();
    }
    private void Awake()
    {
        _safeZoneMechanics = GetComponent<SafeZoneMechanics>();
        _safeZonePlayerDetector = GetComponent<SafeZonePlayerDetector>();
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

    private void OnPlayerEnter(PlayerController _enteredPlayerController)
    {
        _safeZoneMechanics.StartHealPlayerCharacter(_enteredPlayerController);
    }

    private void OnPlayerExit()
    {
        _safeZoneMechanics.StopHealPlayerCharacter();
    }
}
