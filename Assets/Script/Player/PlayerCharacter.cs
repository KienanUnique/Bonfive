using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _maximumHP;
    public int CurrentHP { get; private set; }
    public delegate void OnDie();
    public event OnDie Die;

    private void Awake()
    {
        CurrentHP = _maximumHP;
    }

    public void TakeHit(int damage)
    {
        if (CurrentHP < 0){
            return;
        }
        CurrentHP -= damage;
        if (CurrentHP < 0)
        {
            Die?.Invoke();
        }
        Debug.Log($"HP: -{damage}, Now: {CurrentHP}");
    }

    public void AcceptHealing(int healedHP)
    {
        if (CurrentHP < 0){
            return;
        }
        CurrentHP += healedHP;
        if (CurrentHP > _maximumHP)
        {
            CurrentHP = _maximumHP;
        }
        Debug.Log($"HP: +{healedHP}, Now: {CurrentHP}");
    }
}
