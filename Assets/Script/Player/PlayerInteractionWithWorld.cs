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
            Debug.Log($"IsNull: {_pickedUpFirewood == null}, in zone: {_interactebleObjectsDetector.HasFirewoodInReachebleZone}");
        }

        public void StopInterracrtingWithCurrentObjects()
        {
            if (_pickedUpFirewood != null)
            {
                DropFirewood();
            }
        }

        private void PickUpFirewood()
        {
            _pickedUpFirewood = _interactebleObjectsDetector.FirewoodInReachebleZone;
            _pickedUpFirewood.Destroyed += OnFirewoodDestroyedInFire;
            _pickedUpFirewood.ProcessPickUp(transform);
        }

        private void DropFirewood()
        {
            _pickedUpFirewood.Destroyed -= OnFirewoodDestroyedInFire;
            _pickedUpFirewood.ProcessDrop();
            _pickedUpFirewood = null;
        }

        private void OnEnable()
        {
            if (_pickedUpFirewood != null)
            {
                _pickedUpFirewood.Destroyed += OnFirewoodDestroyedInFire;
            }
        }

        private void OnDisable()
        {
            if (_pickedUpFirewood != null)
            {
                _pickedUpFirewood.Destroyed -= OnFirewoodDestroyedInFire;
            }
        }

        private void OnFirewoodDestroyedInFire()
        {
            Debug.Log("OnFirewoodDestroyedInFire");
            _interactebleObjectsDetector.RemoveDestroyedFirewoodFromList(_pickedUpFirewood);
            _pickedUpFirewood.Destroyed -= OnFirewoodDestroyedInFire;
            _pickedUpFirewood = null;
        }
    }
}