using Assets.Script.Player;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private int _maximumEnemiesCount;
    [SerializeField] private int _maximumFirewoodsCount;
    public static Transform PlayerTransform;
    public static EnemiesRegistrator GlobalEnemiesRegistrator { get; private set; }
    public static FirewoodsRegistrator GlobalFirewoodsRegistrator { get; private set; }
    public static EnemySpawnersRegistrator GlobalEnemySpawnersRegistrator { get; private set; }
    public static FirewoodSpawnersRegistrator GlobalFirewoodSpawnersRegistrator { get; private set; }
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
    }

    private void OnEnable()
    {
        GlobalEnemiesRegistrator.ObjectAdd += ControlEnemiesCount;
        GlobalEnemiesRegistrator.ObjectRemove += ControlEnemiesCount;
        GlobalFirewoodsRegistrator.ObjectAdd += ControlFirewoodsCount;
        GlobalFirewoodsRegistrator.ObjectRemove += ControlFirewoodsCount;
    }

    private void OnDisable()
    {
        GlobalEnemiesRegistrator.ObjectAdd -= ControlEnemiesCount;
        GlobalEnemiesRegistrator.ObjectRemove -= ControlEnemiesCount;
        GlobalFirewoodsRegistrator.ObjectAdd -= ControlFirewoodsCount;
        GlobalFirewoodsRegistrator.ObjectRemove -= ControlFirewoodsCount;
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
