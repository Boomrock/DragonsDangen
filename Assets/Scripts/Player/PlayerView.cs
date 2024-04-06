using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : CharacterView
{
    [SerializeField] private string _moveDirectionKey;
    [SerializeField] private string _diedFlagKey;
    [SerializeField] private string _hitKey;

    [SerializeField] protected CharacterMover? _characterMover;
    [SerializeField] protected CharacterHealth? _characterHealth;
    [SerializeField] protected CharacterAttacker? _characterAttacker;

    [SerializeField] private GameObject _deathEffect;

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

        if (_characterHealth is not null)
        {
            _characterHealth.OnReciavedDamage += PlayHitAnimation;
            _characterHealth.OnCharacterDied += PlayDeathAnimation;
        }
    }

    private void OnDisable()
    {
        if (_characterMover is not null)
        {
            _characterMover.OnMovementDirectionComputed -= ComputePlayerDirectionToAnimator;
        }

        if (_characterHealth is not null)
        {
            _characterHealth.OnReciavedDamage -= PlayHitAnimation;
            _characterHealth.OnCharacterDied -= PlayDeathAnimation;
        }
    }

    private void ComputePlayerDirectionToAnimator(Vector2 direction) => _animator.SetFloat(_moveDirectionKey, direction.magnitude);

    private void PlayHitAnimation() => _animator.SetTrigger(_hitKey);

    private void PlayDeathAnimation() => _animator.SetBool(_diedFlagKey, false);
    private void InstanceDeathEffect() => Instantiate(_deathEffect, transform.position, Quaternion.identity);
}
