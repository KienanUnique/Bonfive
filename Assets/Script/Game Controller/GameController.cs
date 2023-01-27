using System.Collections;
using Assets.Script.Player;
using UnityEngine;

[RequireComponent(typeof(CountDownTimer))]
[RequireComponent(typeof(AllFirewoodsManager))]
[RequireComponent(typeof(AllSkeletonsManager))]
[RequireComponent(typeof(AllMushroomsManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ScenesSwitcher _scenesSwitcher;
    [SerializeField] private UiVisualController _uiVisualController;
    [SerializeField] private int _maximumEnemiesCount;
    [SerializeField] private int _maximumFirewoodsCount;
    [SerializeField] private int _soulsCountForWinning;
    [SerializeField] private float _loadEndingSceneDelaySeconds;
    public static Transform PlayerTransform { get; private set; }
    private EnemiesSoulsCounter _enemiesSoulsCounter;
    private CountDownTimer _endGameTimer;
    private AllFirewoodsManager _allFirewoodsManager;
    private AllSkeletonsManager _allSkeletonsManager;
    private AllMushroomsManager _allMushroomsManager;
    private bool _isGameNotFinished = true;

    private enum GameEndings
    {
        Win, LooseDead, LooseTimeOut
    }

    private void Awake()
    {
        PlayerTransform = _player.transform;
        _enemiesSoulsCounter = new EnemiesSoulsCounter();
        _allFirewoodsManager = GetComponent<AllFirewoodsManager>();
        _endGameTimer = GetComponent<CountDownTimer>();
        _allSkeletonsManager = GetComponent<AllSkeletonsManager>();
        _allMushroomsManager = GetComponent<AllMushroomsManager>();
    }

    private void Start()
    {
        _enemiesSoulsCounter.SetNeedSoulsCount(_soulsCountForWinning);
    }

    private void OnEnable()
    {
        _allSkeletonsManager.EnemyDie += OnEnemieRemove;
        _allMushroomsManager.EnemyDie += OnEnemieRemove;
        _enemiesSoulsCounter.NeedSoulsCountReach += OnNeedSoulsCountReach;
        _endGameTimer.Finish += OnEndGameTimerFinish;
        _player.Die += OnPlayerDie;
        _player.HpChange += OnHpChange;
        _enemiesSoulsCounter.SoulsCountChange += OnSoulsCountChange;
        _endGameTimer.Tick += OnEndGameTimerTick;
    }

    private void OnDisable()
    {
        _allSkeletonsManager.EnemyDie -= OnEnemieRemove;
        _allMushroomsManager.EnemyDie -= OnEnemieRemove;
        _enemiesSoulsCounter.NeedSoulsCountReach -= OnNeedSoulsCountReach;
        _endGameTimer.Finish -= OnEndGameTimerFinish;
        _player.Die -= OnPlayerDie;
        _player.HpChange -= OnHpChange;
        _enemiesSoulsCounter.SoulsCountChange -= OnSoulsCountChange;
        _endGameTimer.Tick -= OnEndGameTimerTick;
    }

    private void OnEndGameTimerFinish()
    {
        EndGame(GameEndings.LooseTimeOut);
    }
    private void OnNeedSoulsCountReach()
    {
        EndGame(GameEndings.Win);
    }
    private void OnPlayerDie()
    {
        EndGame(GameEndings.LooseDead);
    }

    private void EndGame(GameEndings ending)
    {
        if (_isGameNotFinished)
        {
            _allFirewoodsManager.DisableSpawning();
            _allSkeletonsManager.DisableSpawning();
            _allSkeletonsManager.DisableAllACtionsForAllEnemies();
            _allMushroomsManager.DisableSpawning();
            _allMushroomsManager.DisableAllACtionsForAllEnemies();
            _isGameNotFinished = false;
            StartCoroutine(LoadEndingScene(ending));
        }
    }

    private IEnumerator LoadEndingScene(GameEndings ending)
    {
        yield return new WaitForSeconds(_loadEndingSceneDelaySeconds);
        switch (ending)
        {
            case GameEndings.Win:
                _scenesSwitcher.LoadWinScene();
                break;
            case GameEndings.LooseDead:
                _scenesSwitcher.LoadLooseDeadScene();
                break;
            case GameEndings.LooseTimeOut:
                _scenesSwitcher.LoadLooseTimeOutScene();
                break;
        }
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

    private void OnEnemieRemove()
    {
        _enemiesSoulsCounter.AddSoul();
    }
}
