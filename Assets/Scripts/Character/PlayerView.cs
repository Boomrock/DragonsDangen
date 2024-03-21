using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private string _moveDirectionKey;

    [SerializeField] private PlayerMover _playerMover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerMover.OnMovementDirectionComputed += ComputePlayerDirectionToAnimator;
    }

    private void OnDisable()
    {
        _playerMover.OnMovementDirectionComputed -= ComputePlayerDirectionToAnimator;
    }

    private void ComputePlayerDirectionToAnimator(Vector2 direction) => _animator.SetFloat(_moveDirectionKey, direction.magnitude);
}
