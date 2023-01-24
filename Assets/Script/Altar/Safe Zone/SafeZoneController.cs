using UnityEngine;

[RequireComponent(typeof(SafeZoneMechanics))]
[RequireComponent(typeof(SafeZonePhysics))]
public class SafeZoneController : MonoBehaviour
{
    [SerializeField] private SafeZonePlayerDetector _safeZonePlayerDetector;
    private SafeZoneMechanics _safeZoneMechanics;
    private SafeZonePhysics _safeZonePhysics;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _safeZonePhysics.SetupSteps(stepsCount, startStep);
    }
    public void IncreaseSafeZone()
    {
        _safeZonePhysics.IncreaseForce();
    }

    public void DecreaseSafeZone()
    {
        _safeZonePhysics.DecreaseForce();
    }

    public void HealPlayer()
    {
        if (_safeZonePlayerDetector.IsPlayerInside)
        {
            _safeZoneMechanics.HealPlayer(_safeZonePlayerDetector.Player);
        }
    }
    private void Awake()
    {
        _safeZoneMechanics = GetComponent<SafeZoneMechanics>();
        _safeZonePhysics = GetComponent<SafeZonePhysics>();
    }
}
