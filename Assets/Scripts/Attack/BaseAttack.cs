using UnityEngine;

public class BaseAttack : Attack
{
    public BaseAttack(GameObject shell, MonoBehaviour context, GameObjectPool pool) : base(shell, context, pool) { }

    public override void MakeAttack(Vector2 direction)
    {
        var shellPrefab = _pool.Get();

        var shell = shellPrefab.GetComponent<Shell>();

        Debug.Log(_context.name);
        shell.Initialize(direction, _context.gameObject.GetComponent<Collider2D>(), _pool.Return);

    }
}
