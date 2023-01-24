using UnityEngine;

[RequireComponent(typeof(PointEffector2D))]
public class SafeZonePhysics : MonoBehaviour
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
        _pointEffector2D.forceMagnitude = _minimumForceMagnitude + _forceMagnitudeStep * startStep;

        _colliderRadiusStep = (_maximumColliderRadius - _minimumColliderRadius) / stepsCount;
        _collider.radius = _minimumColliderRadius + _colliderRadiusStep * startStep;
    }

    public void IncreaseForce()
    {
        float newforceMagnitude = _pointEffector2D.forceMagnitude + _forceMagnitudeStep;
        if (newforceMagnitude <= _maximumForceMagnitude)
        {
            _pointEffector2D.forceMagnitude = newforceMagnitude;
        }

        float newColliderRadius = _collider.radius + _colliderRadiusStep;
        if (newColliderRadius <= _maximumColliderRadius)
        {
            _collider.radius = newColliderRadius;
        }
    }

    public void DecreaseForce()
    {
        float newforceMagnitude = _pointEffector2D.forceMagnitude - _forceMagnitudeStep;
        if (newforceMagnitude >= _minimumForceMagnitude)
        {
            _pointEffector2D.forceMagnitude = newforceMagnitude;
        }
        float newColliderRadius = _collider.radius - _colliderRadiusStep;
        if (newColliderRadius >= _minimumColliderRadius)
        {
            _collider.radius = newColliderRadius;
        }
    }

}
