using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : CharacterMover
{
    public override event Action<Vector2> OnMovementDirectionComputed;

    private PlayerInput _input;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input.Disable();

    private void Update()
    {
        Vector2 direction = ReadInnputValues();

        OnMovementDirectionComputed?.Invoke(direction);

        if(!direction.Equals(_lastDirectionValues))
        {
            Move(direction);
        }

        _lastDirectionValues = direction;
    }

    protected override void Move(Vector2 inputDirection)
    {
        Vector2 normolizedDirection = inputDirection.normalized;

        _rigidbody.velocity = normolizedDirection * _speed;
        
        if((_isFacedRight && normolizedDirection.x < 0)
            || (!_isFacedRight && normolizedDirection.x > 0))
        {
            Flip();
        }
    }

    private Vector2 ReadInnputValues() => _input.Player.Move.ReadValue<Vector2>();
}
