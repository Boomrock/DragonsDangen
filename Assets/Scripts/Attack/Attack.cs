using UnityEngine;

public abstract class Attack
{
    protected GameObject _shell;

    protected GameObjectPool _pool;

    protected MonoBehaviour _context;

    public Attack(GameObject shell, MonoBehaviour context, GameObjectPool pool)
    {
        _shell = shell;
        _context = context;
        _pool = pool;
    }

    public abstract void MakeAttack(Vector2 direction);
}
