using System.Collections;
using UnityEngine;

public class AllObjectsManager<SpawnersType, ObjectsType> : MonoBehaviour
where SpawnersType : ISpawner
{
    [SerializeField] int _requaredCount;
    public static AllObjectsManager<SpawnersType, ObjectsType> Instance { get; private set; } = null;
    protected static ObjectRegistrator<ObjectsType> _objectsRegistrator;
    protected static SpawnersRegistrator<SpawnersType> _spawnersRegistrator;
    private bool _isAlreadyBalancingCount = false;
    private bool _isSpawinigDisabled = false;
    private const float BalacingUpdateSeconds = 1f;
    private IEnumerator _balanceCount;

    public void DisableSpawning()
    {
        _isSpawinigDisabled = true;
        StopBalanceCountCorutine();
    }

    public static void Registrate(ObjectsType newFirewood)
    {
        _objectsRegistrator.Add(newFirewood);
    }

    public static void Registrate(SpawnersType newSpawner)
    {
        _spawnersRegistrator.Add(newSpawner);
    }

    public static void Remove(ObjectsType firewood)
    {
        _objectsRegistrator.Remove(firewood);
    }

    public static void Remove(SpawnersType spawner)
    {
        _spawnersRegistrator.Remove(spawner);
    }

    private void Awake()
    {
        _objectsRegistrator = new ObjectRegistrator<ObjectsType>();
        _spawnersRegistrator = new SpawnersRegistrator<SpawnersType>();
        Instance = this;
    }

    private void OnEnable()
    {
        _objectsRegistrator.ObjectRemove += OnForewoodRemoved;
    }

    private void OnDisable()
    {
        _objectsRegistrator.ObjectRemove -= OnForewoodRemoved;
    }

    private void Start()
    {
        StartBalanceCountCorutine();
    }

    private void OnForewoodRemoved()
    {
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
        while (_objectsRegistrator.Count < _requaredCount && !_isSpawinigDisabled)
        {
            for (int i = 0; i < (_requaredCount - _objectsRegistrator.Count) && _spawnersRegistrator.HaveReadyToUseSpawner && !_isSpawinigDisabled; i++)
            {
                _spawnersRegistrator.SpawnInRandomAvailebleSpawner();
            }
            yield return new WaitForSeconds(BalacingUpdateSeconds);
        }
        _isAlreadyBalancingCount = false;
    }
}
