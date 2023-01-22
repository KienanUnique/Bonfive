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

        public void RemoveDestroyedFirewoodFromList(FirewoodController destroyedFirewood)
        {
            _firewoodControllers.Remove(destroyedFirewood);
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
}