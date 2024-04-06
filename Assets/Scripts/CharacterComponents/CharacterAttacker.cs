using System.Collections;
using UnityEngine;

public abstract class CharacterAttacker : MonoBehaviour
{
    [SerializeField] protected bool _canShoot;

    [SerializeField] protected int _startPoolSize;

    [SerializeField] protected float _cooldown;

    [SerializeField] protected GameObject _shell;
    [SerializeField] protected GameObjectPool _pool;

    protected Attack _attackType;

    protected abstract void Attack();

    protected virtual IEnumerator MakeCooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _canShoot = true;
    }
}
