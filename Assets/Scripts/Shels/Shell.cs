using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shell : MonoBehaviour
{
    public event Action OnCollidWhithSomething;

    [SerializeField] protected float _speed;

    protected Vector2 _moveDirection;

    protected Action<GameObject> _onReachTargetAction;

    protected Rigidbody2D _rigidbody;

    protected Collider2D _senderCollision;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    public void Initialize(Vector2 direction, Collider2D senderCollider, Action<GameObject> onReachTargetAction)
    {
        _senderCollision = senderCollider;
        _moveDirection = direction;
        _onReachTargetAction = onReachTargetAction;

        transform.position = senderCollider.transform.position;
        transform.rotation = GetRotationByDirection(direction);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != _senderCollision)
        {
            OnCollidWhithSomething?.Invoke();
            _onReachTargetAction(gameObject);
        }
    }

    protected abstract void Move(Vector2 diretcion);

    private Quaternion GetRotationByDirection(Vector2 direction)
    {
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angel, Vector3.forward);
    }
}
