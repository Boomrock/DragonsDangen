using UnityEngine;
using Zenject;

public class GameplayScenInstaller : MonoInstaller
{
    [SerializeField] private PlayerView _playerView;
    public override void InstallBindings()
    {
        PlayerInstaller.Install(Container, _playerView);
    }
}
