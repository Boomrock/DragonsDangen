using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerAttacker : MonoBehaviour
{
    private bool _canShoot;

    [SerializeField] private int _startPoolSize;

    [SerializeField] private float _cooldown;

    [SerializeField] private GameObject _shell;
    [SerializeField] private GameObjectPool _pool;

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
        
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _canShoot = true; 
    }
}
