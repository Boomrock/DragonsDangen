using UnityEngine;

public class CharacterView : MonoBehaviour 
{
    protected Animator _animator;

    protected virtual void Awake() => _animator = GetComponent<Animator>();
}
