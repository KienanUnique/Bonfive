using System.Collections;
using UnityEngine;

public class FirewoodSpawnerController : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject[] _firewoodsVariations;
    [SerializeField] private float _minimumSpawnDelaySeconds;
    [SerializeField] private float _maximumSpawnDelaySeconds;
    [SerializeField] private FirewoodSpawnerSpawnZoneHandler _firewoodSpawnerSpawnZoneHandler;
    private GameObject RandomFirewood => _firewoodsVariations[Random.Range(0, _firewoodsVariations.Length)];
    private float RandomDelayTime => Random.Range(_minimumSpawnDelaySeconds, _maximumSpawnDelaySeconds);
    private IEnumerator _randomDelayedFirewoodSpawn;
    private bool _canSpawn = true;

    public void StopSpawning()
    {
        _canSpawn = false;
        StopCoroutine(_randomDelayedFirewoodSpawn);
    }

    public void StartSpawning()
    {
        _canSpawn = true;
        _randomDelayedFirewoodSpawn = RandomDelayedFirewoodSpawn();
        StartCoroutine(_randomDelayedFirewoodSpawn);
    }

    private void Start()
    {
        GameController.GlobalFirewoodSpawnersRegistrator.Add(this);
    }

    private void OnDestroy()
    {
        GameController.GlobalFirewoodSpawnersRegistrator.Remove(this);
    }

    private IEnumerator RandomDelayedFirewoodSpawn()
    {
        while (_canSpawn)
        {
            yield return new WaitForSeconds(RandomDelayTime);
            if (_canSpawn && _firewoodSpawnerSpawnZoneHandler.IsSpawnZoneEmpty)
            {
                Instantiate(RandomFirewood, transform.position, Quaternion.identity);
            }
        }

    }
}
