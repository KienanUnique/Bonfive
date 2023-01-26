using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIPlayerVisual : MonoBehaviour
{
    private Animator _playerAnimator;
    private static readonly int StartAnimationHash = Animator.StringToHash("StartAnimation");

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        _playerAnimator.SetTrigger(StartAnimationHash);
    }
}
