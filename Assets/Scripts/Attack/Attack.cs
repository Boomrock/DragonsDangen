using UnityEngine;

public abstract class Attack
{
    protected Shell _shell;

    public Attack(Shell shell) => _shell = shell;

    public abstract void MakeAttack(Vector2 direction);
}
