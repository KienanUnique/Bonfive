using UnityEngine;
using Assets.Script.Player;
using System.Collections;

public class SafeZoneMechanics : MonoBehaviour
{
    [SerializeField] private int _countOfHealingHP;

    public void HealPlayer(PlayerController playerController){
        playerController.AcceptHealing(_countOfHealingHP);
    }
}
