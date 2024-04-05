using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAttacker : CharacterAttacker
{
    private PlayerInput _input;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;

        var rigidbody = GetComponent<Rigidbody2D>();

        _pool = new GameObjectPool(_startPoolSize, _shell); 
        _attackType = new BaseAttack(_shell, this, _pool);
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Attack.performed += context => HandleMouseClick();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void HandleMouseClick()
    {
        if (!_canShoot)
        {
            return;
        }

        Attack();
    }

    protected override void Attack()
    {
        var clickPosition = GetLocalClickPosition();
        var direction = clickPosition - transform.position;

        _attackType.MakeAttack(direction);
        _canShoot = false;

        StartCoroutine(nameof(MakeCooldown));
    }

    private Vector3 GetLocalClickPosition()
    {
        var globalClickPosition = Mouse.current.position.ReadValue();

        return Camera.main.ScreenToWorldPoint(globalClickPosition);
    }
}
