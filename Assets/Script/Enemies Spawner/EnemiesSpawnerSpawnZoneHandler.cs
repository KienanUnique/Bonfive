using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemiesSpawnerSpawnZoneHandler : MonoBehaviour
{
    private BoxCollider2D _spawnZone;
    private List<EnemyInterfaceObject> _enemyInterfaceObjects = new List<EnemyInterfaceObject>();
    public bool IsSpawnZoneEmpty => _enemyInterfaceObjects.Count == 0;

    private void Awake()
    {
        _spawnZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyInterfaceObject enemyController) && !_enemyInterfaceObjects.Contains(enemyController))
        {
            _enemyInterfaceObjects.Add(enemyController);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyInterfaceObject enemyInterfaceObject) && _enemyInterfaceObjects.Contains(enemyInterfaceObject))
        {
            _enemyInterfaceObjects.Remove(enemyInterfaceObject);
        }
    }
}
