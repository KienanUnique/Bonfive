using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AttackZoneEnemiesDetector : MonoBehaviour
{
    public List<EnemyController> EnemiesInAttackZone { get; private set; } = new List<EnemyController>();
    public void ClearEnemiesInAttackZoneList(){
        EnemiesInAttackZone.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && !EnemiesInAttackZone.Contains(enemyController))
        {
            EnemiesInAttackZone.Add(enemyController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && EnemiesInAttackZone.Contains(enemyController))
        {
            EnemiesInAttackZone.Remove(enemyController);
        }
    }
}
