public class AllEnemiesManager : AllObjectsManager<EnemiesSpawnerController, EnemyController>
{
    public delegate void OnEnemyDie();
    public event OnEnemyDie EnemyDie;

    public void DisableAllACtionsForAllEnemies()
    {
        var enemiesRegistrator = _objectsRegistrator as EnemiesRegistrator;
        enemiesRegistrator.DisableAllACtionsForAllEnemies();
    }
}
