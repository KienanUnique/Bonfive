using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerVisual : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int RespawnHash = Animator.StringToHash("Respawn");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int UseActionHash = Animator.StringToHash("UseAction");
        private static readonly int TakeHitHash = Animator.StringToHash("Take Hit");
        private static readonly int WinHash = Animator.StringToHash("Win");
        private bool _isFacingRight;
        private bool _isMoving;
        private bool _isDead = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void FlipX()
        {
            _isFacingRight = !_isFacingRight;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void StartMovingAnimation(Vector2 direction)
        {
            if (!_isMoving && !_isDead)
            {
                _isMoving = true;
                _animator.SetBool(IsMovingHash, true);
            }
            if ((direction.x >= 0 && _isFacingRight) || (direction.x < 0 && !_isFacingRight))
            {
                FlipX();
            }
        }

        public void StartIdleAnimation()
        {
            if (_isMoving && !_isDead)
            {
                _isMoving = false;
                _animator.SetBool(IsMovingHash, false);
            }
        }

        public void StartDieAnimation()
        {
            if (_isDead)
            {
                return;
            }
            _isDead = true;
            _animator.ResetTrigger(RespawnHash);
            _animator.ResetTrigger(UseActionHash);
            _animator.ResetTrigger(TakeHitHash);
            _animator.SetBool(IsMovingHash, false);
            _animator.SetTrigger(DieHash);
        }

        public void StartRespawnAnimation()
        {
            _isDead = false;
            _animator.SetTrigger(RespawnHash);
        }

        public void StartUseActionAnimation()
        {
            if (_isDead)
            {
                return;
            }
            _animator.SetTrigger(UseActionHash);
        }

        public void StartTakeHitAnimation()
        {
            if (_isDead)
            {
                return;
            }
            _animator.SetTrigger(TakeHitHash);
        }

        public void PlayUseAnimation(InputAction.CallbackContext callbackContext)
        {
            if (_isDead)
            {
                return;
            }
            _animator.SetTrigger(UseActionHash);
        }

        public void PlayWinAnimation()
        {
            if (_isDead)
            {
                return;
            }
            _animator.ResetTrigger(RespawnHash);
            _animator.ResetTrigger(UseActionHash);
            _animator.ResetTrigger(TakeHitHash);
            _animator.SetBool(IsMovingHash, false);
            _animator.SetTrigger(WinHash);
        }
    }
}