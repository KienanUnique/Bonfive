using UnityEngine;
public class SpawnersRegistrator<T> : ObjectRegistrator<T> where T : ISpawner
{
    public bool HaveReadyToUseSpawner => _objectsList.Exists(spawner => spawner.IsReadyToUse);
    public void SpawnInRandomAvailebleSpawner()
    {
        var readyToUseSpawners = _objectsList.FindAll(spawner => spawner.IsReadyToUse);
        readyToUseSpawners[Random.Range(0, readyToUseSpawners.Count)].Spawn();
    }
}
