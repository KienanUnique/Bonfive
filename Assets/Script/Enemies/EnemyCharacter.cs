using Assets.Script.Player;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private int _enemyDamage;

    public void DamagePlayer(PlayerController player) { 
        player.TakeHit(_enemyDamage);
    }
}
