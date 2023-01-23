using System.Collections.Generic;
using Assets.Script.InteractableItems.Firewood;
using UnityEngine;


namespace Assets.Script.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractebleObjectsDetector : MonoBehaviour
    {
        private List<FirewoodController> _firewoodControllers = new List<FirewoodController>();
        public bool HasFirewoodInReachebleZone => _firewoodControllers.Count != 0;
        public FirewoodController FirewoodInReachebleZone => _firewoodControllers[_firewoodControllers.Count - 1];

        private void OnEnable()
        {
            foreach (var firewood in _firewoodControllers)
            {
                firewood.Destroyed += OnFirewoodDestroy;
            }
        }

        private void OnDisable()
        {
            foreach (var firewood in _firewoodControllers)
            {
                firewood.Destroyed -= OnFirewoodDestroy;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out FirewoodController interactableItemController) && !_firewoodControllers.Contains(interactableItemController))
            {
                _firewoodControllers.Add(interactableItemController);
                interactableItemController.Destroyed += OnFirewoodDestroy;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out FirewoodController interactableItemController) && _firewoodControllers.Contains(interactableItemController))
            {
                interactableItemController.Destroyed -= OnFirewoodDestroy;
                _firewoodControllers.Remove(interactableItemController);
            }
        }

        private void OnFirewoodDestroy(FirewoodController destroyedFirewood)
        {
            destroyedFirewood.Destroyed -= OnFirewoodDestroy;
            _firewoodControllers.Remove(destroyedFirewood);
        }
    }
}