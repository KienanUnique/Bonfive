using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void OnAcceptedRequest();
    public event OnAcceptedRequest AcceptedDisableMovingRequest;
    public event OnAcceptedRequest AcceptedEnableMovingRequest;

    public void EnableMoving(){
        AcceptedEnableMovingRequest?.Invoke();
    }

    public void DisableMoving(){
        AcceptedDisableMovingRequest?.Invoke();
    }
}
