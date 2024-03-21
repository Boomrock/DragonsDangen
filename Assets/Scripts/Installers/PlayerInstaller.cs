using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class PlayerInstaller:Installer<PlayerInstaller>
{
    private static PlayerView _playerView;
    public static void Install(DiContainer container, PlayerView playerView)
    {
        _playerView = playerView;
        Install(container);
    }
    public override void InstallBindings()
    {
        Container
            .Bind<PlayerInput>()
            .FromNew()
            .AsSingle();
        
        Container
            .Bind<Rigidbody2D>()
            .WithId(BindId.Player)
            .FromInstance(_playerView.Rigidbody)
            .AsSingle();
        
        Container
            .Bind<Transform>()
            .WithId(BindId.Player)
            .FromInstance(_playerView.transform);
        Container
            .Bind<PlayerView>()
            .WithId(BindId.Player)
            .FromInstance(_playerView);

        Container
            .Bind<Mover>()
            .WithId(BindId.Player)
            .To<PlayerMover>()
            .AsSingle();
        
        Container
            .Bind<PlayerController>()
            .WithId(BindId.Player)
            .AsSingle()
            .NonLazy();
    }
}
