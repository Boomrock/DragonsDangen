using UnityEngine;

[RequireComponent(typeof(Shell))]
public class ShellView : MonoBehaviour
{
    private Shell _shell;

    [SerializeField] private GameObject _destructionEffect;

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
        Instantiate(_destructionEffect, transform.position, Quaternion.identity);
    }
}
