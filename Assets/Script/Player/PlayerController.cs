using Assets.Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerVisual))]
    [RequireComponent(typeof(PlayerInteractionWithWorld))]
    public class PlayerController : MonoBehaviour
    {
        private const float MinimalInputMagnitude = 0.5f;
        private Maincontrols _mainControls;
        private PlayerMovement _playerMovement;
        private PlayerVisual _playerVisual;
        private PlayerInteractionWithWorld _playerInteractionWithWorld;


        private void Awake()
        {
            _mainControls = new Maincontrols();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerVisual = GetComponent<PlayerVisual>();
            _playerInteractionWithWorld = GetComponent<PlayerInteractionWithWorld>();
        }

        private void Start()
        {
            _playerMovement.TeleportToRespawnPoint();
            _playerMovement.StopMoving();
            _playerVisual.StartIdleAnimation();
        }

        private void Update()
        {
            var readDirection = _mainControls.CharacterControl.Move.ReadValue<Vector2>().normalized;
            if (readDirection.magnitude > MinimalInputMagnitude)
            {
                _playerMovement.Move(readDirection);
                _playerVisual.StartMovingAnimation(readDirection);
            }
            else
            {
                _playerMovement.StopMoving();
                _playerVisual.StartIdleAnimation();
            }
        }

        private void OnUseActionPerformed()
        {
            _playerVisual.StartUseActionAnimation();
            _playerInteractionWithWorld.TryInterractWithObjectsInReachableZone();
        }
        private void OnEnable()
        {
            _mainControls.Enable();
            _mainControls.CharacterControl.UseAction.performed += ctx => OnUseActionPerformed();
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.CharacterControl.UseAction.performed -= ctx => OnUseActionPerformed();
        }
    }
}