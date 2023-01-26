using System.Collections;
using Assets.Script.InteractableItems.Firewood;
using UnityEngine;

public class AllFirewoodsManager : MonoBehaviour
{
    [SerializeField] int _requaredCount;
    public static AllFirewoodsManager Instance { get; private set; } = null;
    private static FirewoodsRegistrator _firewoodsRegistrator;
    private static FirewoodSpawnersRegistrator _firewoodSpawnersRegistrator;
    private bool _isAlreadyBalancingCount = false;
    private bool _isSpawinigDisabled = false;
    private const float BalacingUpdateSeconds = 1f;
    private IEnumerator _balanceCount;

    public void DisableSpawning()
    {
        _isSpawinigDisabled = true;
        StopBalanceCountCorutine();
    }

    public static void Registrate(FirewoodController newFirewood)
    {
        _firewoodsRegistrator.Add(newFirewood);
    }

    public static void Registrate(FirewoodSpawnerController newSpawner)
    {
        _firewoodSpawnersRegistrator.Add(newSpawner);
    }

    public static void Remove(FirewoodController firewood)
    {
        _firewoodsRegistrator.Remove(firewood);
    }

    public static void Remove(FirewoodSpawnerController spawner)
    {
        _firewoodSpawnersRegistrator.Remove(spawner);
    }

    private void Awake()
    {
        _firewoodsRegistrator = new FirewoodsRegistrator();
        _firewoodSpawnersRegistrator = new FirewoodSpawnersRegistrator();
        Instance = this;
    }

    private void OnEnable()
    {
        _firewoodsRegistrator.ObjectRemove += OnForewoodRemoved;
    }

    private void OnDisable()
    {
        _firewoodsRegistrator.ObjectRemove -= OnForewoodRemoved;
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
        while (_firewoodsRegistrator.Count < _requaredCount && !_isSpawinigDisabled)
        {
            for (int i = 0; i < (_requaredCount - _firewoodsRegistrator.Count) && _firewoodSpawnersRegistrator.HaveReadyToUseSpawner && !_isSpawinigDisabled; i++)
            {
                _firewoodSpawnersRegistrator.SpawnInRandomAvailebleSpawner();
            }
            yield return new WaitForSeconds(BalacingUpdateSeconds);
        }
        _isAlreadyBalancingCount = false;
    }
}
