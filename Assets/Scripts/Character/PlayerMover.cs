using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnMovementDirectionComputed;

    [SerializeField, Range(0, 10)] private float _speed;

    private Vector2 _lastDirectionValues;

    private PlayerInput _input;
    private PlayerController _controller;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;

        var rigidbody = GetComponent<Rigidbody2D>();

        _controller = new PlayerController(rigidbody, transform);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

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

    private void Move(Vector2 inputDirection)
    {
        Vector2 normolizedDirection = inputDirection.normalized;

        _controller.Move(normolizedDirection, _speed);
    }

    private Vector2 ReadInnputValues() => _input.Player.Move.ReadValue<Vector2>();
}
