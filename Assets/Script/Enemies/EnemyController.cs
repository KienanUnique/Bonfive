using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void OnAcceptedRequest();
    public delegate void OnInitialize(Transform targetTransform);
    public event OnInitialize Initialize;
    public event OnAcceptedRequest AcceptedDisableMovingRequest;
    public event OnAcceptedRequest AcceptedEnableMovingRequest;
    public event OnAcceptedRequest AcceptedDieRequest;
    public event OnAcceptedRequest AcceptedDisableAllActionsRequest;
    private bool _isQuitting = false;

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

    public void DisableAllActions()
    {
        AcceptedDisableAllActionsRequest?.Invoke();
    }

    public void Die()
    {
        AcceptedDieRequest?.Invoke();
    }

    private void Start()
    {
        AllEnemiesManager.Registrate(this);
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting && AllEnemiesManager.Instance != null)
        {
            AllEnemiesManager.Remove(this);
        }
    }
}
