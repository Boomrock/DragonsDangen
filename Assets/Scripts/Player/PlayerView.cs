using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : CharacterView
{
    [SerializeField] private string _moveDirectionKey;

    private void OnEnable()
    {
        if (_characterMover is not null) 
        {
            _characterMover.OnMovementDirectionComputed += ComputePlayerDirectionToAnimator;
        }
    }

    private void OnDisable()
    {
        if (_characterMover is not null)
        {
            _characterMover.OnMovementDirectionComputed -= ComputePlayerDirectionToAnimator;
        }
    }

    private void ComputePlayerDirectionToAnimator(Vector2 direction) => _animator.SetFloat(_moveDirectionKey, direction.magnitude);
}
