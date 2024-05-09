using HypercasualPrototype.UI;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuController _menuController;

        /// <summary>Service composition</summary>
        public override void InstallBindings()
        {
            Container.Bind<IMenuControllerService>()
                .To<MenuController>()
                .FromInstance(_menuController)
                .AsSingle();
        }
    }
}