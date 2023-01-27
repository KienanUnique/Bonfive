public class FlyingEyeSpawnerController : EnemiesSpawnerController
{
    protected override void RegistrateAction()
    {
        if (AllFlyingEyesManager.Instance != null)
        {
            AllFlyingEyesManager.Registrate(this);
        }
    }

    protected override void RemoveRegistratationAction()
    {
        if (AllFlyingEyesManager.Instance != null)
        {
            AllFlyingEyesManager.Remove(this);
        }
    }
}
