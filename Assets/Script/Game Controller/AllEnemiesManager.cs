public class AllEnemiesManager : AllObjectsManager<EnemiesSpawnerController, EnemyController>
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
            enemy.DisableMoving();
        }
    }

    public void DisableAllACtionsForAllEnemies()
    {
        IsMovingEnabled = false;
        IsAllActionsEnabled = false;
        foreach (var enemy in _spawningObjects)
        {
            enemy.DisableAllActions();
        }
    }

    protected override void RegistrateObjectSpecialAction(EnemyController objectToAdd)
    {
        if (IsAllActionsEnabled)
        {
            if (IsMovingEnabled)
            {
                objectToAdd.EnableMoving();
            }
            else
            {
                objectToAdd.DisableMoving();
            }
        }
        else
        {
            objectToAdd.DisableAllActions();
        }
    }

    protected virtual void RemoveObjectSpecialAction(EnemyController removedObject)
    {
        EnemyDie?.Invoke();
    }
}
