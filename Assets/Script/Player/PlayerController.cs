using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerVisual))]
    public class PlayerController : MonoBehaviour
    {
        private const float MinimalInputMagnitude = 0.5f;
        private Maincontrols _mainControls;
        private PlayerMovement _playerMovement;
        private PlayerVisual _playerVisual;


        private void Awake()
        {
            _mainControls = new Maincontrols();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerVisual = GetComponent<PlayerVisual>();
        }

        private void Update()
        {
            var readDirection = _mainControls.CharacterControl.Move.ReadValue<Vector2>().normalized;
            if (readDirection.magnitude > MinimalInputMagnitude)
            {
                _playerMovement.Move(readDirection);
                _playerVisual.StartMovingAnimation(readDirection);
            }
            else{
                _playerMovement.StopMoving();
                _playerVisual.StartIdleAnimation();
            }
        }

        private void OnEnable()
        {
            _mainControls.Enable();
        }

        private void OnDisable()
        {
            _mainControls.Disable();
        }
    }
}