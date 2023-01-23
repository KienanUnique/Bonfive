using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private float _endWaitSeconds;
    public delegate void OnTick(int timeRemainingMinutes, int timeRemainingSeconds);
    public delegate void OnFinish();
    public event OnTick Tick;
    public event OnFinish Finish;
    private float _timeRemainingSeconds;
    private bool _haveFinished;

    private void Start()
    {
        _timeRemainingSeconds = _endWaitSeconds;
        _haveFinished = false;
    }

    void Update()
    {
        if (!_haveFinished)
        {
            _timeRemainingSeconds -= Time.deltaTime;
            if (_timeRemainingSeconds > 0)
            {
                Tick?.Invoke(Mathf.FloorToInt((int)_timeRemainingSeconds / 60), Mathf.FloorToInt((int)_timeRemainingSeconds % 60));
            }
            else
            {
                _haveFinished = true;
                _timeRemainingSeconds = 0;
                Finish?.Invoke();
            }
        }
    }
}
