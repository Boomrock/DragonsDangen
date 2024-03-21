using UnityEngine;
using Zenject;

public class PlayerMover : Mover, ITickable
{
    private PlayerInput _input;
    private readonly TickableManager _tickableManager;

    private Vector2 _lastDirectionValues;

    [Inject]
    private PlayerMover(
        [Inject(Id = BindId.Player)] Rigidbody2D rigidbody2D, 
        [Inject(Id = BindId.Player)] Transform transform, 
        TickableManager tickableManager,
        PlayerInput input): 
        base(rigidbody2D, transform)
    {
        _input = input;
        _tickableManager = tickableManager;
    }

    public void Enable()
    {
        _input.Enable();
        _tickableManager.Add(this);
    }

    public void Disable()
    {
        _input.Disable();
        _tickableManager.Remove(this);
    }

    public void Tick()
    {
        Vector2 direction = ReadInnputValues();
        
        if(direction.Equals(_lastDirectionValues)) return;
        
        Move(direction);
        _lastDirectionValues = direction;
    }
    private Vector2 ReadInnputValues() => _input.Player.Move.ReadValue<Vector2>();
}


