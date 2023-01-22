using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemiesSpawnerSpawnZoneHandler : MonoBehaviour
{
    private BoxCollider2D _spawnZone;
    private List<EnemyController> _enemyControllers = new List<EnemyController>();
    public bool IsSpawnZoneEmpty => _enemyControllers.Count == 0;

    private void Awake()
    {
        _spawnZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && !_enemyControllers.Contains(enemyController))
        {
            _enemyControllers.Add(enemyController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController) && _enemyControllers.Contains(enemyController))
        {
            _enemyControllers.Remove(enemyController);
        }
    }
}
