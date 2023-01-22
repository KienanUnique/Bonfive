using UnityEngine;
using Assets.Script.Player;

[RequireComponent(typeof(CircleCollider2D))]
public class SafeZoneCharactersDetector : MonoBehaviour
{
    [SerializeField] private float _minimumDetectionRadius;
    [SerializeField] private float _maximumDetectionRadius;
    public delegate void OnPlayerEnter(PlayerController _enteredPlayerController);
    public delegate void OnPlayerExit();
    public event OnPlayerEnter PlayerEnter;
    public event OnPlayerExit PlayerExit;
    private CircleCollider2D _charactersDetectorZone;
    private float _detectionRadiusStep;

    private void Awake()
    {
        _charactersDetectorZone = GetComponent<CircleCollider2D>();
    }

    public void SetupSteps(int stepsCount, int startStep)
    {
        _detectionRadiusStep = (_maximumDetectionRadius - _minimumDetectionRadius) / stepsCount;
        _charactersDetectorZone.radius = _minimumDetectionRadius + _detectionRadiusStep * startStep;
    }
    public void IncreaseDetectionRadius()
    {
        float newDetectorZoneRadius = _charactersDetectorZone.radius + _detectionRadiusStep;
        if (newDetectorZoneRadius <= _maximumDetectionRadius)
        {
            _charactersDetectorZone.radius = newDetectorZoneRadius;
        }
    }

    public void DecreaseDetectionRadius()
    {
        float newDetectorZoneRadius = _charactersDetectorZone.radius - _detectionRadiusStep;
        if (newDetectorZoneRadius >= _minimumDetectionRadius)
        {
            _charactersDetectorZone.radius = newDetectorZoneRadius;
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
