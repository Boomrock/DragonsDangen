using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public override event Action OnReciavedDamage;
    public override event Action OnCharacterDied;

    public override void GetDamage(int damage)
    {
        int tempHealthResult = _health - damage;

        if(tempHealthResult <= 0)
        {
            _health = 0;
            OnCharacterDied?.Invoke();

            return;
        }

        _health = tempHealthResult;

        OnReciavedDamage?.Invoke();
    }
}
