using Zenject;

public class EnemyIstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemyController>().FromComponentInChildren().AsSingle();
        Container.Bind<WeaponController>().FromComponentInChildren().AsSingle();
        Container.Bind<Vitals>().FromComponentInChildren().AsSingle();
    }
}