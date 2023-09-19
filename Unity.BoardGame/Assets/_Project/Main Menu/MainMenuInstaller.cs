using Assets._Project.Main_Menu.Map_Selection;
using Finite_State_Machine;
using UnityEngine;
using Zenject;

namespace Assets._Project.Main_Menu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform
            _hudContainer,
            _popupsContainer;

        public override void InstallBindings()
        {
            BindContainers();
            BindUI();
            BindStates();
            BindControllers();
            BindRunner();
        }

        private void BindControllers()
        {
            Container
                .Bind<MapSelectionController>()
                .FromNew()
                .AsSingle();
        }

        private void BindRunner()
        {
            Container
                .BindInterfacesAndSelfTo<MainMenuRunner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindStates()
        {
            Container
                .Bind<IState>()
                .To<SelectMapState>()
                .FromNew()
                .AsSingle();
        }

        private void BindUI()
        {
            Container
                .Bind<MapSelectionPopupLoader>()
                .FromNew()
                .AsSingle();
        }

        private void BindContainers()
        {
            Container
                .Bind<Transform>()
                .WithId("HUD")
                .FromInstance(_hudContainer);

            Container
                .Bind<Transform>()
                .WithId("Popup")
                .FromInstance(_popupsContainer);
        }
    }
}
