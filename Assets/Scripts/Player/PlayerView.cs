using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : CharacterView
{
    [SerializeField] private string _moveDirectionKey;

    [SerializeField] protected CharacterMover? _characterMover;
    [SerializeField] protected CharacterHealth? _characterHealth;
    [SerializeField] protected CharacterAttacker? _characterAttacker;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent<CharacterMover>(out _characterMover);
        TryGetComponent<CharacterHealth>(out _characterHealth);
        TryGetComponent<CharacterAttacker>(out _characterAttacker);
    }

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
