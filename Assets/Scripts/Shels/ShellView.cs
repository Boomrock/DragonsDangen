using UnityEngine;

public class ShellView : MonoBehaviour
{
    private Shell _shell;

    private void Awake() => _shell = GetComponent<Shell>();

    private void OnEnable()
    {
        _shell.OnCollidWhithSomething += CreateDestructionParticles;
    }

    private void OnDisable()
    {
        _shell.OnCollidWhithSomething -= CreateDestructionParticles;
    }

    private void CreateDestructionParticles()
    {

    }
}
