public class EnemiesRegistrator : ObjectRegistrator<EnemyController>
{
    public bool IsMovingEnabled { get; private set; } = true;
    public bool IsAllActionsEnabled { get; private set; } = true;
    public void DisableMovingForAllEnemies()
    {
        IsMovingEnabled = false;
        foreach (var enemy in _objectsList)
        {
            enemy.DisableMoving();
        }
    }

    public void DisableAllACtionsForAllEnemies()
    {
        IsMovingEnabled = false;
        IsAllActionsEnabled = false;
        foreach (var enemy in _objectsList)
        {
            enemy.DisableAllActions();
        }
    }

    public override void Add(EnemyController objectToAdd)
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
        base.Add(objectToAdd);
    }
}