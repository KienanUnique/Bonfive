using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttackDamageZonePlayerDetector : MonoBehaviour
{
    public PlayerController Player { get; private set; } = null;
    public bool IsPlayerInside => Player != null;
    private CircleCollider2D _spawnZone;

    private void Awake()
    {
        _spawnZone = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Player = playerController;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Player = null;
        }
    }
}
