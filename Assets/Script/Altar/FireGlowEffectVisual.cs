using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireGlowEffectVisual : MonoBehaviour
{
    [SerializeField] private float _minimumGlowEffectSize;
    [SerializeField] private float _maximumGlowEffectSize;
    private float _startGlowEffectSize;
    private float _glowEffectSizeStep;
    private float CurrentGlowEffectSize => transform.localScale.x;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _glowEffectSizeStep = (_maximumGlowEffectSize - _minimumGlowEffectSize) / stepsCount;
        SetNewGlowEffectSize(_minimumGlowEffectSize + startStep * _glowEffectSizeStep);
    }

    public void IncreaseGlowEffectRadius()
    {
        float newGlowEffectSize = CurrentGlowEffectSize + _glowEffectSizeStep;
        if (newGlowEffectSize <= _maximumGlowEffectSize)
        {
            SetNewGlowEffectSize(newGlowEffectSize);
        }
    }
    public void DecreaseGlowEffectRadius()
    {
        float newGlowEffectSize = CurrentGlowEffectSize - _glowEffectSizeStep;
        if (newGlowEffectSize >= _minimumGlowEffectSize)
        {
            SetNewGlowEffectSize(newGlowEffectSize);
        }
    }

    private void SetNewGlowEffectSize(float newSize)
    {
        transform.localScale = new Vector3(newSize, newSize, transform.localScale.z);
    }
}
