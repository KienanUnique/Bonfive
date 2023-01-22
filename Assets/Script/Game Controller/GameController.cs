using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CountDownTimer))]
public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private int _maximumEnemiesCount;
    [SerializeField] private int _maximumFirewoodsCount;
    [SerializeField] private int _soulsCountForWinning;
    public static Transform PlayerTransform;
    public static EnemiesRegistrator GlobalEnemiesRegistrator { get; private set; }
    public static FirewoodsRegistrator GlobalFirewoodsRegistrator { get; private set; }
    public static EnemySpawnersRegistrator GlobalEnemySpawnersRegistrator { get; private set; }
    public static FirewoodSpawnersRegistrator GlobalFirewoodSpawnersRegistrator { get; private set; }
    private static EnemiesSoulsCounter _enemiesSoulsCounter;
    private static CountDownTimer _endGameTimer;
    public static GameController Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {
        PlayerTransform = _player.transform;
        GlobalEnemiesRegistrator = new EnemiesRegistrator();
        GlobalFirewoodsRegistrator = new FirewoodsRegistrator();
        GlobalEnemySpawnersRegistrator = new EnemySpawnersRegistrator();
        GlobalFirewoodSpawnersRegistrator = new FirewoodSpawnersRegistrator();
        _enemiesSoulsCounter = new EnemiesSoulsCounter(_soulsCountForWinning);
        _endGameTimer = GetComponent<CountDownTimer>();
    }

    private void OnEnable()
    {
        GlobalEnemiesRegistrator.ObjectAdd += OnEnemieAdd;
        GlobalEnemiesRegistrator.ObjectRemove += OnEnemieRemove;
        GlobalFirewoodsRegistrator.ObjectAdd += OnFirewoodCountChange;
        GlobalFirewoodsRegistrator.ObjectRemove += OnFirewoodCountChange;
        _enemiesSoulsCounter.NeedSoulsCountReach += OnNeedSoulsCountReach;
        _endGameTimer.Finish += OnEndGameTimerFinish;
        _player.Die += OnPlayerDie;
    }

    private void OnDisable()
    {
        GlobalEnemiesRegistrator.ObjectAdd -= OnEnemieAdd;
        GlobalEnemiesRegistrator.ObjectRemove -= OnEnemieRemove;
        GlobalFirewoodsRegistrator.ObjectAdd -= OnFirewoodCountChange;
        GlobalFirewoodsRegistrator.ObjectRemove -= OnFirewoodCountChange;
        _enemiesSoulsCounter.NeedSoulsCountReach -= OnNeedSoulsCountReach;
        _endGameTimer.Finish -= OnEndGameTimerFinish;
        _player.Die -= OnPlayerDie;
    }

    private void OnEndGameTimerFinish()
    {
        Debug.Log("EndGameTimerFinish");
    }
    private void OnNeedSoulsCountReach()
    {
        Debug.Log("NeedSoulsCountReach");
    }
    private void OnPlayerDie()
    {
        Debug.Log("PlayerDie");
    }

    private void OnFirewoodCountChange()
    {
        ControlFirewoodsCount();
    }

    private void OnEnemieAdd()
    {
        ControlEnemiesCount();
    }

    private void OnEnemieRemove()
    {
        _enemiesSoulsCounter.AddSoul();
        ControlEnemiesCount();
    }

    private void ControlEnemiesCount()
    {
        if (GlobalEnemiesRegistrator.Count >= _maximumEnemiesCount && GlobalEnemySpawnersRegistrator.IsEnabled)
        {
            GlobalEnemySpawnersRegistrator.DisableAllSpawners();
        }
        else if (GlobalEnemiesRegistrator.Count < _maximumEnemiesCount && !GlobalEnemySpawnersRegistrator.IsEnabled)
        {
            GlobalEnemySpawnersRegistrator.EnableAllSpawners();
        }
    }

    private void ControlFirewoodsCount()
    {
        if (GlobalFirewoodsRegistrator.Count >= _maximumFirewoodsCount && GlobalFirewoodSpawnersRegistrator.IsEnabled)
        {
            GlobalFirewoodSpawnersRegistrator.DisableAllSpawners();
        }
        else if (GlobalFirewoodsRegistrator.Count < _maximumFirewoodsCount && !GlobalFirewoodSpawnersRegistrator.IsEnabled)
        {
            GlobalFirewoodSpawnersRegistrator.EnableAllSpawners();
        }
    }
}
