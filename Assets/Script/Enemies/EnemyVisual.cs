using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyVisual : MonoBehaviour
{
    public delegate void OnAnimationMoment();
    public event OnAnimationMoment AttackAnimationHitMomentStart;
    public event OnAnimationMoment MoveAnimationStepMoment;

    private Animator _animator;
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private bool _isFacingRight = false;
    private bool _isMoving = false;
    private bool _isMovingEnabled = true;
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

    public void CallEventAttackAnimationHitMomentStart()
    {
        AttackAnimationHitMomentStart?.Invoke();
    }

    public void CallEventMoveAnimationStepMoment()
    {
        MoveAnimationStepMoment?.Invoke();
    }

    public void StartMovingAnimation(Vector2 direction)
    {
        if (!_isMoving && _isMovingEnabled && !_isDead)
        {
            _isMoving = true;
            _animator.SetBool(IsMovingHash, true);
        }
        if ((direction.x > 0 && _isFacingRight) || (direction.x < 0 && !_isFacingRight))
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
        _isDead = true;
        _animator.ResetTrigger(AttackHash);
        _animator.SetTrigger(DieHash);
    }

    public void StartAttackAnimation()
    {
        if (!_isDead)
        {
            _animator.SetTrigger(AttackHash);
        }
    }

    public void EnableMoving()
    {
        _isMovingEnabled = true;
    }
    public void DisableMoving()
    {
        _isMovingEnabled = false;
        StartIdleAnimation();
    }
}
