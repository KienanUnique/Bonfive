using UnityEngine;
using Assets.Script.Player;

[RequireComponent(typeof(CircleCollider2D))]
public class SafeZonePlayerDetector : MonoBehaviour
{
    [SerializeField] private float _minimumDetectionRadius;
    [SerializeField] private float _maximumDetectionRadius;
    public delegate void OnPlayerEntered(PlayerController _enteredPlayerController);
    public event OnPlayerEntered PlayerEnter;
    public delegate void OnPlayerExit();
    public event OnPlayerExit PlayerExit;
    private CircleCollider2D _playerDetectorZone;
    private float _detectionRadiusStep;

    private void Awake()
    {
        _playerDetectorZone = GetComponent<CircleCollider2D>();
    }

    public void SetupSteps(int stepsCount, int startStep){
        _detectionRadiusStep = (_maximumDetectionRadius - _minimumDetectionRadius) / stepsCount;
        _playerDetectorZone.radius = _minimumDetectionRadius + _detectionRadiusStep * startStep;
    }
    public void IncreasePlayerDetectionRadius()
    {
        float newDetectorZoneRadius = _playerDetectorZone.radius + _detectionRadiusStep;
        if (newDetectorZoneRadius <= _maximumDetectionRadius)
        {
            _playerDetectorZone.radius = newDetectorZoneRadius;
        }
    }

    public void DecreasePlayerDetectionRadius()
    {
        float newDetectorZoneRadius = _playerDetectorZone.radius - _detectionRadiusStep;
        if (newDetectorZoneRadius >= _minimumDetectionRadius)
        {
            _playerDetectorZone.radius = newDetectorZoneRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            PlayerEnter?.Invoke(playerController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            PlayerExit?.Invoke();
        }
    }
}
