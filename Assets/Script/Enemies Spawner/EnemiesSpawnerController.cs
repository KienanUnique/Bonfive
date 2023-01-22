using System.Collections;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject[] _enemiesVariations;
    [SerializeField] private float _minimumSpawnDelaySeconds;
    [SerializeField] private float _maximumSpawnDelaySeconds;
    [SerializeField] private EnemiesSpawnerSpawnZoneHandler _enemiesSpawnerSpawnZoneHandler;
    private GameObject RandomEnemy => _enemiesVariations[Random.Range(0, _enemiesVariations.Length)];
    private float RandomDelayTime => Random.Range(_minimumSpawnDelaySeconds, _maximumSpawnDelaySeconds);
    private IEnumerator _randomDelayedEnemySpawn;
    private bool _canSpawn = true;

    public void StopSpawning()
    {
        _canSpawn = false;
        StopCoroutine(_randomDelayedEnemySpawn);
    }

    public void StartSpawning()
    {
        _canSpawn = true;
        _randomDelayedEnemySpawn = RandomDelayedEnemySpawn();
        StartCoroutine(_randomDelayedEnemySpawn);
    }

    private void Start()
    {
        GameController.GlobalEnemySpawnersRegistrator.Add(this);
    }

    private void OnDestroy()
    {
        GameController.GlobalEnemySpawnersRegistrator.Remove(this);
    }

    private IEnumerator RandomDelayedEnemySpawn()
    {
        while (_canSpawn)
        {
            yield return new WaitForSeconds(RandomDelayTime);
            if (_canSpawn && _enemiesSpawnerSpawnZoneHandler.IsSpawnZoneEmpty)
            {
                GameObject spawnedEnemy = Instantiate(RandomEnemy, transform.position, Quaternion.identity);
                if (spawnedEnemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    enemyController.InitializeEnemyParameters(GameController.PlayerTransform);
                }
            }
        }

    }
}
