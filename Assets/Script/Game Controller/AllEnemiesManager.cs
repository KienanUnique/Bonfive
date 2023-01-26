using System.Collections;
using UnityEngine;

public class AllEnemiesManager : MonoBehaviour
{
    [SerializeField] int _requaredCount;
    public static AllEnemiesManager Instance { get; private set; } = null;
    public delegate void OnEnemyDie();
    public event OnEnemyDie EnemyDie;
    private static EnemiesRegistrator _enemiesRegistrator;
    private static EnemySpawnersRegistrator _enemiesSpawnersRegistrator;
    private bool _isAlreadyBalancingCount = false;
    private bool _isSpawinigDisabled = false;
    private const float BalacingUpdateSeconds = 1f;
    private IEnumerator _balanceCount;

    public void DisableSpawning()
    {
        _isSpawinigDisabled = true;
        StopBalanceCountCorutine();
    }

    public void DisableAllACtionsForAllEnemies(){
        _enemiesRegistrator.DisableAllACtionsForAllEnemies();
    }

    public static void Registrate(EnemyController newEnemy)
    {
        _enemiesRegistrator.Add(newEnemy);
    }

    public static void Registrate(EnemiesSpawnerController newSpawner)
    {
        _enemiesSpawnersRegistrator.Add(newSpawner);
    }

    public static void Remove(EnemyController enemy)
    {
        _enemiesRegistrator.Remove(enemy);
    }

    public static void Remove(EnemiesSpawnerController spawner)
    {
        _enemiesSpawnersRegistrator.Remove(spawner);
    }

    private void Awake()
    {
        _enemiesRegistrator = new EnemiesRegistrator();
        _enemiesSpawnersRegistrator = new EnemySpawnersRegistrator();
        Instance = this;
    }

    private void OnEnable()
    {
        _enemiesRegistrator.ObjectRemove += OnEnemyRemoved;
    }

    private void OnDisable()
    {
        _enemiesRegistrator.ObjectRemove -= OnEnemyRemoved;
    }

    private void Start()
    {
        StartBalanceCountCorutine();
    }

    private void OnEnemyRemoved()
    {
        EnemyDie?.Invoke();
        if (!_isAlreadyBalancingCount)
        {
            StartBalanceCountCorutine();
        }
    }

    private void StartBalanceCountCorutine()
    {
        _balanceCount = BalanceCount();
        StartCoroutine(_balanceCount);
    }

    private void StopBalanceCountCorutine()
    {
        StopCoroutine(_balanceCount);
        _isAlreadyBalancingCount = false;
        _balanceCount = null;
    }

    private IEnumerator BalanceCount()
    {
        _isAlreadyBalancingCount = true;
        while (_enemiesRegistrator.Count < _requaredCount && !_isSpawinigDisabled)
        {
            for (int i = 0; i < (_requaredCount - _enemiesRegistrator.Count) && _enemiesSpawnersRegistrator.HaveReadyToUseSpawner && !_isSpawinigDisabled; i++)
            {
                _enemiesSpawnersRegistrator.SpawnInRandomAvailebleSpawner();
            }
            yield return new WaitForSeconds(BalacingUpdateSeconds);
        }
        _isAlreadyBalancingCount = false;
    }
}
