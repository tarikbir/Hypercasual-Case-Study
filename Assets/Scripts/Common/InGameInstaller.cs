using HypercasualPrototype.UI;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class InGameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private InGameUIController _inGameUIController;
        [SerializeField] private SkillManagerService _skillManagerService;

        /// <summary>Service composition</summary>
        public override void InstallBindings()
        {
            Container.Bind<IPlayerService>()
                .To<PlayerController>()
                .FromInstance(_player)
                .NonLazy();

            Container.Bind<ISkillManagerService>()
                .To<SkillManagerService>()
                .FromInstance(_skillManagerService)
                .AsSingle()
                .Lazy();

            Container.Bind<IInGameUIController>()
                .To<InGameUIController>()
                .FromInstance(_inGameUIController)
                .AsSingle();
        }
    }
}
