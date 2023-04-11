using Zenject;

public class CommonSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ActivityManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<ActivityManagerProxyProvider>().AsSingle().NonLazy();
        Container.Bind<ActivityDepth>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<ActivityManagerProxy>().AsSingle();
    }
}