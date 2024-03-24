using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private bool _canShoot;

    [SerializeField] private int _startPoolSize;

    [SerializeField] private float _cooldown;

    [SerializeField] private GameObject _shell;
    [SerializeField] private GameObjectPool _pool;

    private PlayerInput _input;
    private PlayerController _controller;

    private Attack _attackType;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;

        var rigidbody = GetComponent<Rigidbody2D>();

        _controller = new PlayerController(rigidbody, transform);
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

        HandleAttack();
    }

    private void HandleAttack()
    {
        var clickPosition = GetLocalClickPosition();
        var direction = clickPosition - transform.position;

        _attackType.MakeAttack(direction);
        _canShoot = false;

        StartCoroutine(nameof(Cooldown));
    }

    private Vector3 GetLocalClickPosition()
    {
        var globalClickPosition = Mouse.current.position.ReadValue();

        return Camera.main.ScreenToWorldPoint(globalClickPosition);
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _canShoot = true; 
    }
}
