public class EnemiesSoulsCounter
{
    public delegate void OnNeedSoulsCountReach();
    public event OnNeedSoulsCountReach NeedSoulsCountReach;
    private readonly int _needSoulsCount;
    private int _currentSoulsCount = 0;
    private bool _wasNeedSoulsCountReached = false;

    public EnemiesSoulsCounter(int soulsCountForWinning)
    {
        _needSoulsCount = soulsCountForWinning;
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
    }

}
