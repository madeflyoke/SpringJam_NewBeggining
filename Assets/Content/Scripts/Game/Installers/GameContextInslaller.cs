using Content.Scripts.Game.InputService;
using Content.Scripts.Game.Level;
using Content.Scripts.Game.Player.Characters;
using Content.Scripts.Game.UI;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Game.Installers
{
    public class GameContextInslaller : MonoInstaller
    {
        [Header("Services")] 
        [SerializeField] private InputHandler InputService;
        [SerializeField] private UiContainer UiContainer;
        [SerializeField] private LevelLauncher LevelLauncher;

        [Space(5)] 
        [Header("Configs")]
        [SerializeField] private InputConfig InputConfig;
        [SerializeField] private CharacterTeamConfig CharacterTeamConfig;
        [SerializeField] private CharacterMotionConfig CharacterMotionConfig;
        public override void InstallBindings()
        {
            InstallConfigs();
            InstallServices();
        }

        public void InstallConfigs()
        {
            Container.Bind<InputConfig>()
                .FromInstance(InputConfig)
                .AsSingle()
                .NonLazy();
            Container.Bind<CharacterTeamConfig>()
                .FromInstance(CharacterTeamConfig)
                .AsSingle()
                .NonLazy();
            Container.Bind<CharacterMotionConfig>()
                .FromInstance(CharacterMotionConfig)
                .AsSingle()
                .NonLazy();
        }

        public void InstallServices()
        {
            Container.Bind<InputHandler>()
                .FromInstance(InputService)
                .AsSingle()
                .NonLazy();
            Container.Bind<UiContainer>()
                .FromInstance(UiContainer)
                .AsSingle()
                .NonLazy();
            Container.Bind<LevelLauncher>()
                .FromInstance(LevelLauncher)
                .AsSingle()
                .NonLazy();
        }
    }
}