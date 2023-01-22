using UnityEngine;

namespace Assets.Script.InteractableItems.Firewood
{
    [RequireComponent(typeof(FirewoodMovement))]
    [RequireComponent(typeof(FirewoodVisual))]
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class FirewoodController : MonoBehaviour
    {
        public delegate void OnDestroyedInFire();
        public event OnDestroyedInFire DestroyedInFire;
        private Rigidbody2D _rigidbody2D;
        private FirewoodMovement _firewoodMovement;
        private FirewoodVisual _firewoodVisual;
        private void Awake()
        {
            _firewoodMovement = GetComponent<FirewoodMovement>();
            _firewoodVisual = GetComponent<FirewoodVisual>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void ProcessPickUp(Transform targetToFollow)
        {
            _firewoodMovement.StartFollowPosition(targetToFollow);
            _firewoodVisual.ProcessPickUp();
            _rigidbody2D.simulated = false;
        }

        public void ProcessDrop()
        {
            _firewoodMovement.StopFollowingPosition();
            _firewoodVisual.ProcessDropDown();
            _rigidbody2D.simulated = true;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void DestroyInFire(){
            DestroyedInFire?.Invoke();
            Destroy(gameObject);
        }

    }
}