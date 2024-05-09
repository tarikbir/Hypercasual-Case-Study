using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioService;

        /// <summary>Service composition</summary>
        public override void InstallBindings()
        {
            Container.Bind<IDataService>()
                .To<DataService>()
                .AsSingle();

            Container.Bind<IGameManagerService>()
                .To<GameManagerService>()
                .FromInstance(GetComponent<GameManagerService>())
                .AsSingle()
                .NonLazy();

            Container.Bind<IAudioService>()
                .To<AudioService>()
                .FromInstance(_audioService)
                .AsSingle();
        }
    }
}