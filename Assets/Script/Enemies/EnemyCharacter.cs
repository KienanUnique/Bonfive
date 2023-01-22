using Assets.Script.Player;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private int _enemyDamage;
    public bool IsAlive {get; private set;} = true;

    public void DamagePlayer(PlayerController player) { 
        player.TakeHit(_enemyDamage);
    }

    public void Die(){
        IsAlive = false;
    }
}
