using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private bool _movingEnabled;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction)
    {
        if (_movingEnabled)
        {
            _rigidbody2D.velocity = _moveSpeed * direction;
        }
    }

    public void StopMoving()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }


    public void DisableMoving()
    {
        _movingEnabled = false;
        StopMoving();
    }

    public void EnableMoving()
    {
        _movingEnabled = true;
    }
}
