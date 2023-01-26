using System;
using UnityEngine;

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

    protected override void RegistrateObjectSpecialAction(System.Object newObject)
    {
        var EnemyController = newObject as EnemyController;
        if (IsAllActionsEnabled)
        {
            if (IsMovingEnabled)
            {
                EnemyController.EnableMoving();
            }
            else
            {
                EnemyController.DisableMoving();
            }
        }
        else
        {
            EnemyController.DisableAllActions();
        }
    }

    protected override void RemoveObjectActions(System.Object removedObject)
    {
        EnemyDie?.Invoke();
    }
}
