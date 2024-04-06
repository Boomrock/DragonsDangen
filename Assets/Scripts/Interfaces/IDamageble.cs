using System;

public interface IDamageble
{
    public abstract event Action OnReciavedDamage;

    public abstract void GetDamage(int damage);
}
