using UnityEngine;
using Assets.Script.Player;

[RequireComponent(typeof(CircleCollider2D))]
public class SafeZonePlayerDetector : MonoBehaviour
{
    public PlayerController Player { get; private set; } = null;
    public bool IsPlayerInside => Player != null;

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
