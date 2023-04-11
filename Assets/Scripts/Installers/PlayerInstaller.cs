using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovementSettingsSO _settings;

    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromComponentInChildren().AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
        Container.Bind<CharacterController>().FromComponentInChildren().AsSingle();
        Container.Bind<PlayerMovementSettingsSO>().FromInstance(_settings).AsSingle();
        Container.Bind<WeaponController>().FromComponentInChildren().AsSingle();
        Container.Bind<Vitals>().FromComponentInChildren().AsSingle();
    }
}