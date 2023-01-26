using UnityEngine;

[RequireComponent(typeof(PointEffector2D))]
public class SafeZonePhysics : MonoBehaviour, IStepsControllable
{
    [Header("Effector Force Magnitude")]
    [SerializeField] private float _minimumForceMagnitude;
    [SerializeField] private float _maximumForceMagnitude;

    [Header("Effector Collider Radius")]
    [SerializeField] private float _minimumColliderRadius;
    [SerializeField] private float _maximumColliderRadius;
    private PointEffector2D _pointEffector2D;
    private CircleCollider2D _collider;
    private float _forceMagnitudeStep;
    private float _colliderRadiusStep;

    private void Awake()
    {
        _pointEffector2D = GetComponent<PointEffector2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    public void SetupSteps(int stepsCount, int startStep)
    {
        _forceMagnitudeStep = (_maximumForceMagnitude - _minimumForceMagnitude) / stepsCount;
        _colliderRadiusStep = (_maximumColliderRadius - _minimumColliderRadius) / stepsCount;
        ApplyNewStep(startStep);
    }

    public void ApplyNewStep(int newStep)
    {
        float newColliderRadius, newForceMagnitude;
        if (newStep >= 0)
        {
            newColliderRadius = _minimumColliderRadius + _colliderRadiusStep * newStep;
            newForceMagnitude = _minimumForceMagnitude + _forceMagnitudeStep * newStep;
        }
        else
        {
            newColliderRadius = _minimumColliderRadius;
            newForceMagnitude = 0;
        }
        _collider.radius = newColliderRadius;
        _pointEffector2D.forceMagnitude = newForceMagnitude;
    }
}
