using UnityEngine;

public class BasicShell : Shell
{
    private void Update()
    {
        Move(_moveDirection);
    }

    protected override void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }
}
