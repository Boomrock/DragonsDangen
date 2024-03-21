using Zenject;

public class GameplayScenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromNew().AsSingle();
    }
}
