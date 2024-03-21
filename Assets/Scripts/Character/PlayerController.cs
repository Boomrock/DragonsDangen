using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isFacedRight;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        Vector2 currentScale = transform.localScale;

        transform.localScale = new Vector2(-currentScale.x, currentScale.y);
    }
}
