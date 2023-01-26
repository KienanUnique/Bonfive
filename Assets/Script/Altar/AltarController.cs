using UnityEngine;
using System.Collections;
using Assets.Script.InteractableItems.Firewood;
using System.Collections.Generic;

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
    private int _currentStep;
    private List<IStepsControllable> _stepsControllableObjects;
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
        _countOfSteps--;
        _currentStep = _startStep;
        _stepsControllableObjects = new List<IStepsControllable>();
        _stepsControllableObjects.Add(_safeZoneController);
        _stepsControllableObjects.Add(_fireEffectVisual);
        _stepsControllableObjects.Add(_fireGlowEffectVisual);
        _stepsControllableObjects.Add(_attackZoneController);
        _stepsControllableObjects.ForEach(stepsControllable => stepsControllable.SetupSteps(_countOfSteps, _startStep));
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

    private void UpdateStepsControllableObjects()
    {
        _stepsControllableObjects.ForEach(stepsControllable => stepsControllable.ApplyNewStep(_currentStep));
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
        if (_currentStep < _countOfSteps)
        {
            _currentStep++;
            UpdateStepsControllableObjects();
        }
        _attackZoneController.AttackEnemies();
    }

    private IEnumerator PutFireOutWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(_fadingFireStepSeconds);
            if (_currentStep >= 0)
            {
                _currentStep--;
                UpdateStepsControllableObjects();
            }
        }

    }
}
