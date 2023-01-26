using System.Collections;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject[] _enemiesVariations;
    [SerializeField] private float _spawnCooldownSeconds;
    [SerializeField] private EnemiesSpawnerSpawnZoneHandler _enemiesSpawnerSpawnZoneHandler;
    public bool IsReadyToUse => !_isUnderCooldown && _enemiesSpawnerSpawnZoneHandler.IsSpawnZoneEmpty;
    private GameObject RandomEnemy => _enemiesVariations[Random.Range(0, _enemiesVariations.Length)];
    private bool _isUnderCooldown = false;

    private void Start()
    {
        AllEnemiesManager.Registrate(this);
    }

    private void OnDestroy()
    {
        if(AllEnemiesManager.Instance != null){
            AllEnemiesManager.Remove(this);
        }
    }

    private IEnumerator StartCooldown()
    {
        _isUnderCooldown = true;
        yield return new WaitForSeconds(_spawnCooldownSeconds);
        _isUnderCooldown = false;
    }

    public void Spawn()
    {
        if (IsReadyToUse)
        {
            GameObject spawnedEnemy = Instantiate(RandomEnemy, transform.position, Quaternion.identity);
            if (spawnedEnemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.InitializeEnemyParameters(GameController.PlayerTransform);
            }
            StartCoroutine(StartCooldown());
        }
    }
}
