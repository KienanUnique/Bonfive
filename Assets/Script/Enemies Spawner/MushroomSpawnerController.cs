public class MushroomSpawnerController : EnemiesSpawnerController
{
    protected override void RegistrateAction()
    {
        if (AllMushroomsManager.Instance != null)
        {
            AllMushroomsManager.Registrate(this);
        }
    }

    protected override void RemoveRegistratationAction()
    {
        if (AllMushroomsManager.Instance != null)
        {
            AllMushroomsManager.Remove(this);
        }
    }
}
