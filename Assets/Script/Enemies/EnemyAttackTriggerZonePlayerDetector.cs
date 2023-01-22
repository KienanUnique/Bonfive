using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttackTriggerZonePlayerDetector : MonoBehaviour
{
    public bool IsPlayerInsideTriggerZone => _triggerZone.bounds.Contains(GameController.Instance.Player.CurrentPosition);
    private CircleCollider2D _triggerZone;
    private void Awake()
    {
        _triggerZone = GetComponent<CircleCollider2D>();
    }
}
