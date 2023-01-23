using UnityEngine;

public class UiVisualController : MonoBehaviour
{
    [SerializeField] private UiVisualHp _uiVisualHp;
    [SerializeField] private UiVisualSouls _uiVisualSouls;
    [SerializeField] private UiVisualTimer _uiVisualTimer;

    public void UpdateHp(int currentHp, int maxHp)
    {
        _uiVisualHp.UpdateHp(currentHp, maxHp);
    }
    public void UpdateSouls(int currentSouls, int maxSouls)
    {
        _uiVisualSouls.UpdateSouls(currentSouls, maxSouls);
    }
    public void UpdateTimer(int minutes, int seconds)
    {
        _uiVisualTimer.UpdateTimer(minutes, seconds);
    }
}
