using Assets.Script.InteractableItems.Firewood;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FirewoodSpawnerSpawnZoneHandler : MonoBehaviour
{
    private BoxCollider2D _spawnZone;
    private List<FirewoodController> _firewoodControllers = new List<FirewoodController>();
    public bool IsSpawnZoneEmpty => _firewoodControllers.Count == 0;

    private void Awake()
    {
        _spawnZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out FirewoodController interactableItemController) && !_firewoodControllers.Contains(interactableItemController))
        {
            _firewoodControllers.Add(interactableItemController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out FirewoodController interactableItemController) && _firewoodControllers.Contains(interactableItemController))
        {
            _firewoodControllers.Remove(interactableItemController);
        }
    }


}
