using UnityEngine;

public class AttackZoneDamageInAnimationInvoker : MonoBehaviour
{
    public delegate void OnAttackAnimationHitMomentStart();
    public event OnAttackAnimationHitMomentStart AttackAnimationHitMomentStart;

    public void InvokeAttackAnimationHitMomentStartEvent(){
        AttackAnimationHitMomentStart?.Invoke();
    }
}
