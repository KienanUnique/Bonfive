using System.Collections;
using UnityEngine;

public class FirewoodSpawnerController : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject[] _firewoodsVariations;
    [SerializeField] private float _spawnCooldownSeconds;
    [SerializeField] private FirewoodSpawnerSpawnZoneHandler _firewoodSpawnerSpawnZoneHandler;
    public bool IsReadyToUse => !_isUnderCooldown && _firewoodSpawnerSpawnZoneHandler.IsSpawnZoneEmpty;
    private GameObject RandomFirewood => _firewoodsVariations[Random.Range(0, _firewoodsVariations.Length)];
    private bool _isUnderCooldown = false;

    private void Start()
    {
        AllFirewoodsManager.Registrate(this);
    }

    private void OnDestroy()
    {
        if (AllFirewoodsManager.Instance != null)
        {
            AllFirewoodsManager.Remove(this);
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
            Instantiate(RandomFirewood, transform.position, Quaternion.identity);
            StartCoroutine(StartCooldown());
        }
    }
}
