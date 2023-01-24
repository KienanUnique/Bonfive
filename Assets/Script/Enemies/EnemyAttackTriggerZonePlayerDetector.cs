using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttackTriggerZonePlayerDetector : MonoBehaviour
{
    public bool IsPlayerInsideTriggerZone { get; private set; }

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
