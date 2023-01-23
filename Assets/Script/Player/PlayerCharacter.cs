using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _maximumHP;
    public int CurrentHP { get; private set; }
    public delegate void OnDie();
    public event OnDie Die;
    public delegate void OnHpChange(int currentHp, int maximumHp);
    public event OnHpChange HpChange;

    private void Start()
    {
        CurrentHP = _maximumHP;
        HpChange?.Invoke(CurrentHP, _maximumHP);
    }

    public void TakeHit(int damage)
    {
        if (CurrentHP <= 0)
        {
            return;
        }
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Die?.Invoke();
        }
        HpChange?.Invoke(CurrentHP, _maximumHP);
    }

    public void AcceptHealing(int healedHP)
    {
        if (CurrentHP <= 0)
        {
            return;
        }
        CurrentHP += healedHP;
        if (CurrentHP >= _maximumHP)
        {
            CurrentHP = _maximumHP;
        }
        HpChange?.Invoke(CurrentHP, _maximumHP);
    }
}
