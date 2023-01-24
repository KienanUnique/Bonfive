using UnityEngine;
using System.Collections;
using Assets.Script.InteractableItems.Firewood;

[RequireComponent(typeof(AltarFirewoodDetector))]
[RequireComponent(typeof(AltarAudio))]
public class AltarController : MonoBehaviour
{
    [SerializeField] private SafeZoneController _safeZoneController;
    [SerializeField] private AttackZoneController _attackZoneController;
    [SerializeField] private RunesVisual _runesVisual;
    [SerializeField] private FireGlowEffectVisual _fireGlowEffectVisual;
    [SerializeField] private FireEffectVisual _fireEffectVisual;
    [SerializeField] private int _countOfSteps;
    [SerializeField] private int _startStep;
    [SerializeField] private float _fadingFireStepSeconds;
    private AltarFirewoodDetector _altarFirewoodDetector;
    private AltarAudio _altarAudio;
    private IEnumerator _putFireOutWithDelay;

    private void Awake()
    {
        _altarFirewoodDetector = GetComponent<AltarFirewoodDetector>();
        _altarAudio = GetComponent<AltarAudio>();
    }
    private void Start()
    {
        _safeZoneController.SetupSteps(_countOfSteps, _startStep - 1);
        _fireEffectVisual.SetupSteps(_countOfSteps, _startStep);
        _fireGlowEffectVisual.SetupSteps(_countOfSteps, _startStep);
        _attackZoneController.SetupSteps(_countOfSteps, _startStep);
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
        _altarAudio.PlayFirewoodBurnSound();
        _safeZoneController.HealPlayer();
        StopCoroutine(_putFireOutWithDelay);
        _putFireOutWithDelay = PutFireOutWithDelay();
        StartCoroutine(_putFireOutWithDelay);
        _enteredFirewoodController.DestroyInFire();
        _runesVisual.StartRuneGlowAnimation();
        _safeZoneController.IncreaseSafeZone();
        _fireEffectVisual.IncreaseFireParticleSize();
        _fireGlowEffectVisual.IncreaseGlowEffectRadius();
        _attackZoneController.IncreaseAttackZone();
        _attackZoneController.AttackEnemies();
    }

    private IEnumerator PutFireOutWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(_fadingFireStepSeconds);
            _safeZoneController.DecreaseSafeZone();
            _fireEffectVisual.DecreaseFireParticleSize();
            _fireGlowEffectVisual.DecreaseGlowEffectRadius();
            _attackZoneController.DecreaseAttackZone();
        }

    }
}
