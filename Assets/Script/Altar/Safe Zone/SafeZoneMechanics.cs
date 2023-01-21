using UnityEngine;
using Assets.Script.Player;
using System.Collections;

public class SafeZoneMechanics : MonoBehaviour
{
    [SerializeField] private int _countOfHealingHP;
    [SerializeField] private float _healCooldownSeconds;
    private PlayerController _playerController;
    private IEnumerator _healPlayerWithCooldown;

    private void Awake() {
        _healPlayerWithCooldown = HealPlayerWithCooldown();
    }

    public void StartHealPlayerCharacter(PlayerController playerController){
        _playerController = playerController;
        StartCoroutine(_healPlayerWithCooldown);
    }

    public void StopHealPlayerCharacter(){
        StopCoroutine(_healPlayerWithCooldown);
        _playerController = null;
    }

    private IEnumerator HealPlayerWithCooldown()
    {
        while (true)
        {
            _playerController.Heal(_countOfHealingHP);
            yield return new WaitForSeconds(_healCooldownSeconds);
        }

    }
}
