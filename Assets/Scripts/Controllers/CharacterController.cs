using UnityEngine;

public abstract class CharacterController
{
    protected bool _isFacedRight = true;

    protected Rigidbody2D _rigidbody;

    protected Transform _objectTransform;

    public CharacterController(Rigidbody2D rigidbody, Transform objectTrasfor)
    {
        _rigidbody = rigidbody;
        _objectTransform = objectTrasfor;
    }

    protected abstract void Move(Vector2 moveDirection, float speed);

    protected void Flip()
    {
        Vector2 currentScale = _objectTransform.transform.localScale;

        _objectTransform.transform.localScale = new Vector2(-currentScale.x, currentScale.y);
    }
}
