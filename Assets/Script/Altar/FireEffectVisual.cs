using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireEffectVisual : MonoBehaviour, IStepsControllable
{
    [SerializeField] private float _minimumParticleSize;
    [SerializeField] private float _maximumParticleSize;
    private float _startParticleSize;
    private ParticleSystemRenderer _fireParticleSystemRenderer;
    private ParticleSystem _fireParticleSystem;
    private float _fireParticleSizeStep;
    private float CurrentParticleSizeSize => _fireParticleSystemRenderer.minParticleSize;

    public void SetupSteps(int stepsCount, int startStep)
    {
        _fireParticleSizeStep = (_maximumParticleSize - _minimumParticleSize) / stepsCount;
        ApplyNewStep(startStep);
    }

    public void ApplyNewStep(int newStep)
    {
        if (newStep >= 0)
        {
            if (_fireParticleSystem.isStopped)
            {
                _fireParticleSystem.Play();
            }
            _fireParticleSystemRenderer.minParticleSize = _minimumParticleSize + newStep * _fireParticleSizeStep;
        }
        else
        {
            _fireParticleSystem.Stop();
        }
    }

    private void Awake()
    {
        _fireParticleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        _fireParticleSystem = GetComponent<ParticleSystem>();
    }
}
