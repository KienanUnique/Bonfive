using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireGlowEffectVisual : MonoBehaviour, IStepsControllable
{
    [SerializeField] private float _minimumGlowEffectSize;
    [SerializeField] private float _maximumGlowEffectSize;
    private float _startGlowEffectSize;
    private float _glowEffectSizeStep;
    private float CurrentGlowEffectSize => transform.localScale.x;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _glowEffectSizeStep = (_maximumGlowEffectSize - _minimumGlowEffectSize) / stepsCount;
        ApplyNewStep(startStep);
    }

    public void ApplyNewStep(int newStep)
    {
        var newSize = _minimumGlowEffectSize + newStep * _glowEffectSizeStep;
        transform.localScale = new Vector3(newSize, newSize, transform.localScale.z);
    }
}
