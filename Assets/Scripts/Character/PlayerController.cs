using UnityEngine;

public class PlayerController
{
    private bool _isFacedRight = true;

    private Rigidbody2D _rigidbody;

    private Transform _objectTransform;

    public PlayerController(Rigidbody2D rigidbody, Transform objectTransform)
    {
        _rigidbody = rigidbody;
        _objectTransform = objectTransform;
    }

    public void Attack(Attack attackType, Vector2 direction)
    {

    }

    public void Move(Vector2 moveDirection, float speed)
    {
        _rigidbody.velocity = moveDirection * speed;

        if ((moveDirection.x > 0 && !_isFacedRight)
            || (moveDirection.x < 0 && _isFacedRight))
        {
            _isFacedRight = !_isFacedRight;

            Flip();
        }
    }

    private void Flip()
    {
        Vector2 currentScale = _objectTransform.transform.localScale;

        _objectTransform.transform.localScale = new Vector2(-currentScale.x, currentScale.y);
    }
}
