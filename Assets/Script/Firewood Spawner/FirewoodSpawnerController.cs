using System.Collections;
using UnityEngine;

public class FirewoodSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject[] _firewoodsVariations;
    [SerializeField] private float _minimumSpawnDelaySeconds;
    [SerializeField] private float _maximumSpawnDelaySeconds;
    [SerializeField] private FirewoodSpawnerSpawnZoneHandler _firewoodSpawnerSpawnZoneHandler;
    private GameObject RandomFirewood => _firewoodsVariations[Random.Range(0, _firewoodsVariations.Length)];
    private float RandomDelayTime => Random.Range(_minimumSpawnDelaySeconds, _maximumSpawnDelaySeconds);

    private void Start()
    {
        StartCoroutine(RandomDelayedFirewoodSpawn());
    }

    private IEnumerator RandomDelayedFirewoodSpawn()
    {
        while (true)
        {
            if (_firewoodSpawnerSpawnZoneHandler.IsSpawnZoneEmpty)
            {
                Instantiate(RandomFirewood, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(RandomDelayTime);
        }

    }

}
