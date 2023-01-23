public class EnemiesSoulsCounter
{
    public delegate void OnNeedSoulsCountReach();
    public delegate void OnSoulsCountChange(int currentSoulsCount, int requaredSoulsCount);
    public event OnNeedSoulsCountReach NeedSoulsCountReach;
    public event OnSoulsCountChange SoulsCountChange;
    private int _needSoulsCount;
    private int _currentSoulsCount = 0;
    private bool _wasNeedSoulsCountReached = false;

    public void SetNeedSoulsCount(int soulsCountForWinning)
    {
        _needSoulsCount = soulsCountForWinning;
        SoulsCountChange?.Invoke(_currentSoulsCount, _needSoulsCount);
    }

    public void AddSoul()
    {
        if (_wasNeedSoulsCountReached)
        {
            return;
        }
        _currentSoulsCount++;
        if (_currentSoulsCount >= _needSoulsCount)
        {
            _wasNeedSoulsCountReached = true;
            NeedSoulsCountReach?.Invoke();
        }
        SoulsCountChange?.Invoke(_currentSoulsCount, _needSoulsCount);
    }

}
