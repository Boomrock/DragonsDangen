using UnityEngine;

public class BaseAttack : Attack
{
    public BaseAttack(GameObject shell, MonoBehaviour context, GameObjectPool pool) : base(shell, context, pool) { }

    public override void MakeAttack(Vector2 direction)
    {
        var shellPrefab = _pool.Get();

        var shell = shellPrefab.GetComponent<Shell>();

        shell.Initialize(direction, _context.gameObject.GetComponent<Collision2D>());
    }
}
