using System;
using UnityEngine;

public abstract class Mover
{
    public event Action<Vector2> OnMove;

    protected float _speed = 3f;

    protected readonly Rigidbody2D _rigidbody2D;
    protected readonly Transform _transform;
    protected bool _isFacedRight = true;

    protected Mover(
        Rigidbody2D rigidbody2D, 
        Transform transform)
    {
        _rigidbody2D = rigidbody2D;
        _transform = transform;
    }
    
    protected virtual void Move(Vector2 inputDirection)
    {
        Vector2 normolizedDirection = inputDirection.normalized;
        Move(normolizedDirection, _speed);
        OnMove?.Invoke(inputDirection);
    }

    protected virtual void Move(Vector2 moveDirection, float speed)
    {
        _rigidbody2D.velocity = moveDirection * speed;

        if ((moveDirection.x > 0 && !_isFacedRight)
            || (moveDirection.x < 0 && _isFacedRight))
        {
            _isFacedRight = !_isFacedRight;

            Flip();
        }
    }

    protected void Flip()
    {
        Vector2 currentScale = _transform.localScale;

        _transform.localScale = new Vector2(-currentScale.x, currentScale.y);
    }
}