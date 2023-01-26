public interface ISpawner
{
    public bool IsReadyToUse { get; }
    public void Spawn();
}