using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerView : MonoBehaviour
{
    public Action OnViewEnable, 
                  OnViewDisable;

    public Rigidbody2D Rigidbody => rigidBody;
    
    [SerializeField] private string _moveDirectionKey;
    [SerializeField] private PlayerMover _playerMover;

    [SerializeField] private Animator _animator;
    [FormerlySerializedAs("_rigidBody2d")] [SerializeField] private Rigidbody2D rigidBody;

    private void Awake()
    {
        _animator ??= GetComponent<Animator>();
        rigidBody ??= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OnViewEnable?.Invoke();
    }

    private void OnDisable()
    {
        OnViewDisable?.Invoke();
    }

    internal void ComputePlayerDirectionToAnimator(Vector2 direction) => _animator.SetFloat(_moveDirectionKey, direction.magnitude);
}
