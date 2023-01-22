using UnityEngine;

public class AttackZoneVisual : MonoBehaviour
{
    [SerializeField] private Animator[] _visualEffectsAnimators;
    [SerializeField] private AttackZoneDamageInAnimationInvoker _damageTrigger;
    public delegate void OnAttackAnimationHitMomentStart();
    public event OnAttackAnimationHitMomentStart AttackAnimationHitMomentStart;
    private static readonly int StartHash = Animator.StringToHash("Start");
    public void StartAttackAnimation()
    {
        foreach (var animator in _visualEffectsAnimators)
        {
            animator.SetTrigger(StartHash);
        }
    }

    private void OnEnable() {
        _damageTrigger.AttackAnimationHitMomentStart += InvokeAttackAnimationHitMomentStartEvent;
    }

    private void OnDisable() {
        _damageTrigger.AttackAnimationHitMomentStart -= InvokeAttackAnimationHitMomentStartEvent;
    }

    private void InvokeAttackAnimationHitMomentStartEvent(){
        AttackAnimationHitMomentStart?.Invoke();
    }
}
