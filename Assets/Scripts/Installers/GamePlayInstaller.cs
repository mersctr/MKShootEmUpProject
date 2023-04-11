using Cinemachine;
using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private SurfaceImpactListSO _surfaceImpactListSO;

    public override void InstallBindings()
    {
        Container.Bind<GameMode>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<PoolSystem>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<BulletManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<SurfaceImpactListSO>().FromInstance(_surfaceImpactListSO).AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EnemyManager>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<EffectManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<LevelManager>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<CinemachineTargetGroup>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<EnemyBossController>().FromComponentInHierarchy(true).AsSingle();
    }
}