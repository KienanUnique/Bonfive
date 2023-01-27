using System.Collections;
using UnityEngine;

public abstract class EnemiesSpawnerController : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject[] _enemiesVariations;
    [SerializeField] private float _spawnCooldownSeconds;
    [SerializeField] private EnemiesSpawnerSpawnZoneHandler _enemiesSpawnerSpawnZoneHandler;
    public bool IsReadyToUse => !_isUnderCooldown && _enemiesSpawnerSpawnZoneHandler.IsSpawnZoneEmpty;
    private GameObject RandomEnemy => _enemiesVariations[Random.Range(0, _enemiesVariations.Length)];
    private bool _isUnderCooldown = false;

    protected abstract void RegistrateAction();
    protected abstract void RemoveRegistratationAction();

    private void Start()
    {
        RegistrateAction();
    }

    private void OnDestroy()
    {
        RemoveRegistratationAction();
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
            if (spawnedEnemy.TryGetComponent<EnemyInterfaceObject>(out EnemyInterfaceObject enemyInterfaceObject))
            {
                enemyInterfaceObject.InitializeEnemyParameters(GameController.PlayerTransform);
            }
            StartCoroutine(StartCooldown());
        }
    }
}
