using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private ListOfBulletsSO _bullets;
    [SerializeField] private SurfaceImpactListSO _surfaceImpactListSO;

    public override void InstallBindings()
    {
        Container.Bind<GameMode>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PoolSystem>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<BulletManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<ListOfBulletsSO>().FromInstance(_bullets).AsSingle();
        Container.Bind<SurfaceImpactListSO>().FromInstance(_surfaceImpactListSO).AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EffectManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TargetGroupController>().FromComponentInHierarchy().AsSingle();
    }
}