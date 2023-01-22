using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void OnAcceptedRequest();
    public delegate void OnInitialize(Transform targetTransform);
    public event OnInitialize Initialize;
    public event OnAcceptedRequest AcceptedDisableMovingRequest;
    public event OnAcceptedRequest AcceptedEnableMovingRequest;
    public event OnAcceptedRequest AcceptedDieRequest;

    public void InitializeEnemyParameters(Transform targetTransform)
    {
        Initialize?.Invoke(targetTransform);
    }
    public void EnableMoving()
    {
        AcceptedEnableMovingRequest?.Invoke();
    }

    public void DisableMoving()
    {
        AcceptedDisableMovingRequest?.Invoke();
    }

    public void Die()
    {
        AcceptedDieRequest?.Invoke();
    }

    private void Start()
    {
        GameController.GlobalEnemiesRegistrator.Add(this);
    }

    private void OnDestroy()
    {
        GameController.GlobalEnemiesRegistrator.Remove(this);
    }
}
