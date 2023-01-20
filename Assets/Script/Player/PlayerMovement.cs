using UnityEngine;

namespace Assets.Script.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _moveSpeed = 7f;

        [Header("Die")]
        [SerializeField] private Transform _respawnPoint;

        private BoxCollider2D _boxCollider2D;
        private bool _movingEnabled;
        private bool _isMoving;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _movingEnabled = true;
        }

        public void Move(Vector2 direction)
        {
            if (_movingEnabled)
            {
                _rigidbody2D.velocity = _moveSpeed * direction;
            }
        }

        public void StopMoving()
        {
            ;
            _rigidbody2D.velocity = Vector2.zero;
        }


        public void DisableMoving()
        {
            _movingEnabled = false;
        }

        public void EnableMoving()
        {
            _movingEnabled = true;
        }

        public void TeleportToRespawnPoint()
        {
            transform.position = _respawnPoint.position;
        }
    }
}