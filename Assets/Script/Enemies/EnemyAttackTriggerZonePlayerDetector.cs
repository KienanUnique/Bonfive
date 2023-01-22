using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttackTriggerZonePlayerDetector : MonoBehaviour
{
    public bool IsPlayerInsideTriggerZone {get; private set;}
    private CircleCollider2D _triggerZone;
    private void Awake()
    {
        _triggerZone = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            IsPlayerInsideTriggerZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            IsPlayerInsideTriggerZone = false;
        }
    }
}
