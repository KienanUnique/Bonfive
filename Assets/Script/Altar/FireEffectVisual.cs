using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireEffectVisual : MonoBehaviour, IStepsControllable
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
        ApplyNewStep(startStep);
    }

    public void ApplyNewStep(int newStep)
    {
        _fireParticleSystem.minParticleSize = _minimumParticleSize + newStep * _fireParticleSizeStep;
    }

    private void Awake()
    {
        _fireParticleSystem = GetComponent<ParticleSystemRenderer>();
    }
}
