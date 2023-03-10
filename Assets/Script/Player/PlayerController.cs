using System;
using Assets.Script.InteractableItems.Firewood;
using UnityEngine;

namespace Assets.Script.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerVisual))]
    [RequireComponent(typeof(PlayerCharacter))]
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(PlayerInteractionWithWorld))]
    public class PlayerController : MonoBehaviour
    {
        public delegate void OnCharacterDie();
        public event OnCharacterDie Die;
        public delegate void OnCharacterHpChange(int currentHp, int maximumHp);
        public event OnCharacterHpChange HpChange;
        public Vector2 CurrentPosition => transform.position;
        private const float MinimalInputMagnitude = 0.5f;
        private Maincontrols _mainControls;
        private PlayerMovement _playerMovement;
        private PlayerVisual _playerVisual;
        private PlayerCharacter _playerCharacter;
        private PlayerInteractionWithWorld _playerInteractionWithWorld;
        private PlayerAudio _playerAudio;
        private bool NeedProcesCharacterControl = true;

        public void AcceptHealing(int countOfHP)
        {
            _playerCharacter.AcceptHealing(countOfHP);
        }

        public void TakeHit(int countOfHP)
        {
            _playerAudio.PlayHitSound();
            _playerVisual.StartTakeHitAnimation();
            _playerCharacter.TakeHit(countOfHP);
        }

        public FirewoodController LooseFirewood()
        {
            return _playerInteractionWithWorld.LooseCurrentFirewood();
        }

        public void ProcessGameWin()
        {
            NeedProcesCharacterControl = false;
            _playerVisual.PlayWinAnimation();
            _playerMovement.DisableMoving();
            _playerInteractionWithWorld.StopInterracrtingWithCurrentObjects();
        }

        public void OnAnimationUseActionMomentStart()
        {
            _playerInteractionWithWorld.TryInterractWithObjectsInReachableZone();
            _playerAudio.PlayInteractWitnItemSound();
        }

        private void Awake()
        {
            _mainControls = new Maincontrols();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerVisual = GetComponent<PlayerVisual>();
            _playerInteractionWithWorld = GetComponent<PlayerInteractionWithWorld>();
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerAudio = GetComponent<PlayerAudio>();
        }

        private void Start()
        {
            _playerMovement.TeleportToRespawnPoint();
            _playerMovement.StopMoving();
            _playerVisual.StartIdleAnimation();
        }

        private void Update()
        {
            if (NeedProcesCharacterControl)
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
        }

        private void OnUseActionPerformed()
        {
            if (NeedProcesCharacterControl)
            {
                _playerVisual.StartUseActionAnimation();
            }
        }
        private void OnEnable()
        {
            _mainControls.Enable();
            _mainControls.CharacterControl.UseAction.performed += ctx => OnUseActionPerformed();
            _playerCharacter.Die += OnDie;
            _playerCharacter.HpChange += OnHpChange;
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.CharacterControl.UseAction.performed -= ctx => OnUseActionPerformed();
            _playerCharacter.Die -= OnDie;
            _playerCharacter.HpChange -= OnHpChange;
        }

        private void OnDie()
        {
            NeedProcesCharacterControl = false;
            _playerMovement.DisableMoving();
            _playerVisual.StartDieAnimation();
            _playerAudio.PlayDieSound();
            _playerInteractionWithWorld.StopInterracrtingWithCurrentObjects();
            Die?.Invoke();
        }

        private void OnHpChange(int currentHp, int maximumHp)
        {
            HpChange?.Invoke(currentHp, maximumHp);
        }
    }
}