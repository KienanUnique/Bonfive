using Assets.Script.InteractableItems.Firewood;
using UnityEngine;

namespace Assets.Script.Player
{
    public class PlayerInteractionWithWorld : MonoBehaviour
    {
        [SerializeField] private InteractebleObjectsDetector _interactebleObjectsDetector;
        private Transform _pickedUpItem;
        private FirewoodController _pickedUpFirewood = null;

        public void TryInterractWithObjectsInReachableZone()
        {
            if (_pickedUpFirewood == null && _interactebleObjectsDetector.HasFirewoodInReachebleZone)
            {
                PickUpFirewood();
            }
            else if (_pickedUpFirewood != null)
            {
                DropFirewood();
            }
        }

        public void StopInterracrtingWithCurrentObjects()
        {
            if (_pickedUpFirewood != null)
            {
                DropFirewood();
            }
        }

        public FirewoodController LooseCurrentFirewood()
        {
            var lostFirewood = _pickedUpFirewood;
            if (_pickedUpFirewood != null)
            {
                DropFirewood();
            }
            return lostFirewood;
        }

        private void PickUpFirewood()
        {
            _pickedUpFirewood = _interactebleObjectsDetector.FirewoodInReachebleZone;
            _pickedUpFirewood.ProcessPickUp(transform);
        }

        private void DropFirewood()
        {
            _pickedUpFirewood.ProcessDrop();
            _pickedUpFirewood = null;
        }
    }
}