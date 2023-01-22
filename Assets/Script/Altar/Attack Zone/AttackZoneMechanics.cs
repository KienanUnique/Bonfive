using System.Collections.Generic;
using UnityEngine;

public class AttackZoneMechanics : MonoBehaviour
{
    public void DamageEnemies(List<EnemyController> enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.Die();
        }
    }
}
