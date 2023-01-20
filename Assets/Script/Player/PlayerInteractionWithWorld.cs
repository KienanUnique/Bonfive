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
                _pickedUpFirewood = _interactebleObjectsDetector.FirewoodInReachebleZone;
                _pickedUpFirewood.ProcessPickUp(transform);
            }
            else if (_pickedUpFirewood != null)
            {
                _pickedUpFirewood.ProcessDrop();
                _pickedUpFirewood = null;
            }
        }
    }
}