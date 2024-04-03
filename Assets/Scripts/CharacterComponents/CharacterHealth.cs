using System;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour, IDamageble
{
    public abstract event Action OnReciavedDamage;
    public abstract event Action OnCharacterDied;

    [SerializeField] protected bool _isAlive;

    [SerializeField, Range(0, 10)] protected int _health;

    public abstract void GetDamage(int damage);
}
