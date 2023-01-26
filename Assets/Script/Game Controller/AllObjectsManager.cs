using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjectsManager<SpawnersType, ObjectsType> : MonoBehaviour
where SpawnersType : ISpawner
{
    [SerializeField] int _requaredCount;
    public static AllObjectsManager<SpawnersType, ObjectsType> Instance { get; private set; } = null;
    protected static List<ObjectsType> _spawningObjects;
    protected static List<SpawnersType> _spawners;
    private bool _isAlreadyBalancingCount = false;
    private bool _isSpawinigDisabled = false;
    private const float BalacingUpdateSeconds = 1f;
    private IEnumerator _balanceCount;
    private bool HaveReadyToUseSpawner => _spawners.Exists(spawner => spawner.IsReadyToUse);

    public void DisableSpawning()
    {
        _isSpawinigDisabled = true;
        StopBalanceCountCorutine();
    }

    public static void Registrate(ObjectsType newObject)
    {
        Instance.RegistrateObjectSpecialAction(newObject);
        _spawningObjects.Add(newObject);
    }

    public static void Registrate(SpawnersType newSpawner)
    {
        _spawners.Add(newSpawner);
    }

    public static void Remove(ObjectsType removedObject)
    {
        Instance.RemoveObjectActions(removedObject);
        _spawningObjects.Remove(removedObject);
        if (!Instance._isAlreadyBalancingCount)
        {
            Instance.StartBalanceCountCorutine();
        }
    }

    public static void Remove(SpawnersType spawner)
    {
        _spawners.Remove(spawner);
    }

    protected virtual void RegistrateObjectSpecialAction(ObjectsType newObject) { }
    protected virtual void RemoveObjectActions(ObjectsType removedObject) { }

    private void Awake()
    {
        _spawningObjects = new List<ObjectsType>();
        _spawners = new List<SpawnersType>();
        Instance = this;
    }

    private void Start()
    {
        StartBalanceCountCorutine();
    }

    private void SpawnInRandomAvailebleSpawner()
    {
        var readyToUseSpawners = _spawners.FindAll(spawner => spawner.IsReadyToUse);
        readyToUseSpawners[Random.Range(0, readyToUseSpawners.Count)].Spawn();
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
        while (_spawningObjects.Count < _requaredCount && !_isSpawinigDisabled)
        {
            for (int i = 0; i < (_requaredCount - _spawningObjects.Count) && HaveReadyToUseSpawner && !_isSpawinigDisabled; i++)
            {
                SpawnInRandomAvailebleSpawner();
            }
            yield return new WaitForSeconds(BalacingUpdateSeconds);
        }
        _isAlreadyBalancingCount = false;
    }
}
