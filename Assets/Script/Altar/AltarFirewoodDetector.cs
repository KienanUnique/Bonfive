using Assets.Script.InteractableItems.Firewood;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AltarFirewoodDetector : MonoBehaviour
{
    public delegate void OnFirewoodEntered(FirewoodController _enteredFirewoodController);
    public event OnFirewoodEntered FirewoodEnter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out FirewoodController firewoodController))
        {
            FirewoodEnter?.Invoke(firewoodController);
        }
    }
}
