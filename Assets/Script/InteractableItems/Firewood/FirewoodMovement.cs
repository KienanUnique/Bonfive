using UnityEngine;

namespace Assets.Script.InteractableItems.Firewood
{
    public class FirewoodMovement : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 1.0f;
        [SerializeField] private Vector3 _offset;
        private Transform _targetToFollow = null;
        private bool _needFollow = false;

        public void StartFollowPosition(Transform targetToFollow)
        {
            _needFollow = true;
            _targetToFollow = targetToFollow;
        }

        public void StopFollowingPosition()
        {
            _needFollow = false;
            _targetToFollow = null;
        }

        private void LateUpdate()
        {
            if (!_needFollow) return;
            transform.position = Vector2.Lerp(transform.position, _targetToFollow.position + _offset, _lerpSpeed * Time.deltaTime);
        }
    }
}