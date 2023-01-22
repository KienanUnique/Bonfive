public class EnemiesRegistrator : ObjectRegistrator<EnemyController>{
    public void DisableMovingForAllEnemies(){
        foreach(var enemy in _objectsList){
            enemy.DisableMoving();
        }
    }
}