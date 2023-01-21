using UnityEngine;
using System.Collections;
using Assets.Script.InteractableItems.Firewood;

[RequireComponent(typeof(AltarFirewoodDetector))]
public class AltarController : MonoBehaviour
{
    [SerializeField] private SafeZoneController _safeZoneController;
    [SerializeField] private RunesVisual _runesVisual;
    [SerializeField] private FireGlowEffectVisual _fireGlowEffectVisual;
    [SerializeField] private FireEffectVisual _fireEffectVisual;
    [SerializeField] private int _countOfSteps;
    [SerializeField] private int _startStep;
    [SerializeField] private float _fadingFireStepSeconds;
    private AltarFirewoodDetector _altarFirewoodDetector;
    private IEnumerator _putFireOutWithDelay;

    private void Awake()
    {
        _altarFirewoodDetector = GetComponent<AltarFirewoodDetector>();
    }
    private void Start()
    {
        _safeZoneController.SetupSteps(_countOfSteps, _startStep);
        _fireEffectVisual.SetupSteps(_countOfSteps, _startStep);
        _fireGlowEffectVisual.SetupSteps(_countOfSteps, _startStep);
        _putFireOutWithDelay = PutFireOutWithDelay();
        StartCoroutine(_putFireOutWithDelay);
    }
    private void OnEnable()
    {
        _altarFirewoodDetector.FirewoodEnter += OnFirewoodEnteredAltar;
    }
    private void OnDisable()
    {
        _altarFirewoodDetector.FirewoodEnter -= OnFirewoodEnteredAltar;
    }

    private void OnFirewoodEnteredAltar(FirewoodController _enteredFirewoodController)
    {
        StopCoroutine(_putFireOutWithDelay);
        _putFireOutWithDelay = PutFireOutWithDelay();
        StartCoroutine(_putFireOutWithDelay);
        _enteredFirewoodController.DestroyInFire();
        _runesVisual.StartRuneGlowAnimation();
        _safeZoneController.IncreaseSafeZone();
        _fireEffectVisual.IncreaseFireParticleSize();
        _fireGlowEffectVisual.IncreaseGlowEffectRadius();
    }

    private IEnumerator PutFireOutWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(_fadingFireStepSeconds);
            _safeZoneController.DecreaseSafeZone();
            _fireEffectVisual.DecreaseFireParticleSize();
            _fireGlowEffectVisual.DecreaseGlowEffectRadius();
        }

    }
}
