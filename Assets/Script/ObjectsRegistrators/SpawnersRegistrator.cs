public class SpawnersRegistrator<T> : ObjectRegistrator<T> where T : ISpawner
{
    public bool IsEnabled {get; private set;} = true;
     public void DisableAllSpawners(){
        IsEnabled = false;
        foreach(var spawner in _objectsList){
            spawner.StopSpawning();
        }
    }

    public void EnableAllSpawners(){
        IsEnabled = true;
        foreach(var spawner in _objectsList){
            spawner.StartSpawning();
        }
    }

    public override void Add(T objectToAdd)
    {
        var spawnerObject = objectToAdd as ISpawner;
        if(IsEnabled){
            spawnerObject.StartSpawning();
        }
        else{
            spawnerObject.StopSpawning();
        }
        base.Add(objectToAdd);
    }
}
