using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shell : MonoBehaviour
{
    public event Action OnCollidWhithSomething;

    [SerializeField] protected float _speed;

    protected Vector2 _moveDirection;

    protected Rigidbody2D _rigidbody;

    protected Collision2D _senderCollision;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    public void Initialize(Vector2 direction, Collision2D senderCollision)
    {
        _senderCollision = senderCollision;
        _moveDirection = direction;

        transform.position = senderCollision.transform.position;
    }

    protected abstract void Move(Vector2 diretcion);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != _senderCollision)
        {
            OnCollidWhithSomething?.Invoke();
        }
    }
}
