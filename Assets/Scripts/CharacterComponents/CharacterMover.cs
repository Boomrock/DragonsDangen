using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMover : MonoBehaviour
{
    public abstract event Action<Vector2> OnMovementDirectionComputed;

    [SerializeField] protected bool _isFacedRight;

    [SerializeField] protected float _speed;

    protected Vector2 _lastDirectionValues;

    protected Rigidbody2D _rigidbody;

    protected virtual void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
    
    protected abstract void Move(Vector2 inputDirection);

    protected virtual void Flip()
    {
        Vector2 currentScale = transform.localScale;

        _isFacedRight = !_isFacedRight;
        transform.localScale = new Vector2(-currentScale.x, currentScale.y);
    }
}
