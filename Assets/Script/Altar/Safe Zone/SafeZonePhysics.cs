using UnityEngine;

[RequireComponent(typeof(PointEffector2D))]
public class SafeZonePhysics : MonoBehaviour
{
    [SerializeField] private float _minimumForceMagnitude;
    [SerializeField] private float _maximumForceMagnitude;
    private PointEffector2D _pointEffector2D;
    private float _forceMagnitudeStep;

    private void Awake()
    {
        _pointEffector2D = GetComponent<PointEffector2D>();
    }

    public void SetupSteps(int stepsCount, int startStep)
    {
        _forceMagnitudeStep = (_maximumForceMagnitude - _minimumForceMagnitude) / stepsCount;
        _pointEffector2D.forceMagnitude = _minimumForceMagnitude + _forceMagnitudeStep * startStep;
    }

    public void IncreaseForceMagnitude()
    {
        float newforceMagnitude = _pointEffector2D.forceMagnitude + _forceMagnitudeStep;
        if (newforceMagnitude <= _maximumForceMagnitude)
        {
            _pointEffector2D.forceMagnitude = newforceMagnitude;
        }
    }

    public void DecreaseForceMagnitude()
    {
        float newforceMagnitude = _pointEffector2D.forceMagnitude - _forceMagnitudeStep;
        if (newforceMagnitude >= _minimumForceMagnitude)
        {
            _pointEffector2D.forceMagnitude = newforceMagnitude;
        }
    }

}
