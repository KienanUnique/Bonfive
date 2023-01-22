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
        private bool _isDestroyed = false;

        public void ProcessPickUp(Transform targetToFollow)
        {
            if(_isDestroyed){
                return;
            }
            _firewoodMovement.StartFollowPosition(targetToFollow);
            _firewoodVisual.ProcessPickUp();
            _rigidbody2D.simulated = false;
        }

        public void ProcessDrop()
        {
            if(_isDestroyed){
                return;
            }
            _firewoodMovement.StopFollowingPosition();
            _firewoodVisual.ProcessDropDown();
            _rigidbody2D.simulated = true;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void DestroyInFire(){
            _isDestroyed = true;
            DestroyedInFire?.Invoke();
            Destroy(gameObject);
        }

        private void Awake()
        {
            _firewoodMovement = GetComponent<FirewoodMovement>();
            _firewoodVisual = GetComponent<FirewoodVisual>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            GameController.GlobalFirewoodsRegistrator.Add(this);
        }

        private void OnDestroy() {
            GameController.GlobalFirewoodsRegistrator.Remove(this);
        }

    }
}