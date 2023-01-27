public abstract class AllEnemiesManager<EnemyControllerType> : AllObjectsManager<EnemiesSpawnerController, EnemyControllerType>
where EnemyControllerType : EnemyController
{
    public delegate void OnEnemyDie();
    public event OnEnemyDie EnemyDie;
    public bool IsMovingEnabled { get; private set; } = true;
    public bool IsAllActionsEnabled { get; private set; } = true;

    public void DisableMovingForAllEnemies()
    {
        IsMovingEnabled = false;
        foreach (var enemy in _spawningObjects)
        {
            enemy.InterfaceObject.DisableMoving();
        }
    }

    public void DisableAllACtionsForAllEnemies()
    {
        IsMovingEnabled = false;
        IsAllActionsEnabled = false;
        foreach (var enemy in _spawningObjects)
        {
            enemy.InterfaceObject.DisableAllActions();
        }
    }

    protected override void RegistrateObjectSpecialAction(System.Object newObject)
    {
        var enemyInterfaceObject = (newObject as EnemyController).InterfaceObject;
        if (IsAllActionsEnabled)
        {
            if (IsMovingEnabled)
            {
                enemyInterfaceObject.EnableMoving();
            }
            else
            {
                enemyInterfaceObject.DisableMoving();
            }
        }
        else
        {
            enemyInterfaceObject.DisableAllActions();
        }
    }

    protected override void RemoveObjectActions(System.Object removedObject)
    {
        EnemyDie?.Invoke();
    }
}
