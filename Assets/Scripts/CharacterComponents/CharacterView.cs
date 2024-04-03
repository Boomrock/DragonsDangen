using UnityEngine;

public class CharacterView : MonoBehaviour 
{
    [SerializeField] protected CharacterMover? _characterMover;
    [SerializeField] protected CharacterHealth? _characterHealth;
    [SerializeField] protected CharacterAttacker _characterAttacker;

    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();

        TryGetComponent<CharacterMover>(out _characterMover);
        TryGetComponent<CharacterHealth>(out _characterHealth);
        TryGetComponent<CharacterAttacker>(out _characterAttacker);
    }
}
