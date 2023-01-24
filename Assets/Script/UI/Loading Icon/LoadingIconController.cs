using UnityEngine;

public class LoadingIconController : MonoBehaviour
{
    [SerializeField] private GameObject _loadingIconImageGameObject;
    [SerializeField] private float _rotationSpeed;
    private bool _animationStarted;

    public void StartLoadingAnimation()
    {
        if (_animationStarted)
        {
            return;
        }
        _loadingIconImageGameObject.SetActive(true);
        _animationStarted = true;
    }
    private void Start()
    {
        _loadingIconImageGameObject.SetActive(false);
        _animationStarted = false;
    }

    private void Update()
    {
        if (_animationStarted)
        {
            _loadingIconImageGameObject.transform.Rotate(0, 0, Time.deltaTime * _rotationSpeed, Space.Self);
        }
    }
}
