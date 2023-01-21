using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireEffectVisual : MonoBehaviour
{
    [SerializeField] private float _minimumParticleSize;
    [SerializeField] private float _maximumParticleSize;
    private float _startParticleSize;
    private ParticleSystemRenderer _fireParticleSystem;
    private float _fireParticleSizeStep;
    private float CurrentParticleSizeSize => _fireParticleSystem.minParticleSize;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _fireParticleSizeStep = (_maximumParticleSize - _minimumParticleSize) / stepsCount;
        SetNewFireParticleSize(_minimumParticleSize + startStep * _fireParticleSizeStep);
    }

    public void IncreaseFireParticleSize()
    {
        float newGlowEffectSize = CurrentParticleSizeSize + _fireParticleSizeStep;
        if (newGlowEffectSize <= _maximumParticleSize)
        {
            SetNewFireParticleSize(newGlowEffectSize);
        }
    }
    public void DecreaseFireParticleSize()
    {
        float newGlowEffectSize = CurrentParticleSizeSize - _fireParticleSizeStep;
        if (newGlowEffectSize >= _minimumParticleSize)
        {
            SetNewFireParticleSize(newGlowEffectSize);
        }
    }

    private void Awake()
    {
        _fireParticleSystem = GetComponent<ParticleSystemRenderer>();
    }

    private void SetNewFireParticleSize(float newSize)
    {
        _fireParticleSystem.minParticleSize = newSize;
    }
}
