using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CountDownTimer))]
public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ScenesSwitcher _scenesSwitcher;
    [SerializeField] private UiVisualController _uiVisualController;
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

        Initialize();
    }

    private void Start()
    {
        _enemiesSoulsCounter.SetNeedSoulsCount(_soulsCountForWinning);
    }

    private void Initialize()
    {
        PlayerTransform = _player.transform;
        GlobalEnemiesRegistrator = new EnemiesRegistrator();
        GlobalFirewoodsRegistrator = new FirewoodsRegistrator();
        GlobalEnemySpawnersRegistrator = new EnemySpawnersRegistrator();
        GlobalFirewoodSpawnersRegistrator = new FirewoodSpawnersRegistrator();
        _enemiesSoulsCounter = new EnemiesSoulsCounter();
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
        _player.HpChange += OnHpChange;
        _enemiesSoulsCounter.SoulsCountChange += OnSoulsCountChange;
        _endGameTimer.Tick += OnEndGameTimerTick;
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
        _player.HpChange -= OnHpChange;
        _enemiesSoulsCounter.SoulsCountChange -= OnSoulsCountChange;
        _endGameTimer.Tick -= OnEndGameTimerTick;
    }

    private void OnEndGameTimerFinish()
    {
        ProcessLoose();
    }
    private void OnNeedSoulsCountReach()
    {
        ProcessWin();
    }
    private void OnPlayerDie()
    {
        ProcessLoose();
    }

    private void ProcessWin()
    {
        _scenesSwitcher.LoadWinScene();
    }

    private void ProcessLoose()
    {
        _scenesSwitcher.LoadLooseScene();
    }

    private void OnEndGameTimerTick(int minutes, int seconds)
    {
        _uiVisualController.UpdateTimer(minutes, seconds);
    }

    private void OnHpChange(int currentHp, int maximumHp)
    {
        _uiVisualController.UpdateHp(currentHp, maximumHp);
    }

    private void OnSoulsCountChange(int currentSoulsCount, int requaredSoulsCount)
    {
        _uiVisualController.UpdateSouls(currentSoulsCount, requaredSoulsCount);
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
