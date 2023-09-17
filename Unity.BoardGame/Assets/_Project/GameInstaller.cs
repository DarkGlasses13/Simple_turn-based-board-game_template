using Assets._Project.Character_Selection;
using Assets._Project.Characters;
using Assets._Project.Dice_Rolling;
using Assets._Project.Turn_Sequencing;
using Finite_State_Machine;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    public class GameInstaller : MonoInstaller 
    {
        [SerializeField] private Transform
            _hudContainer,
            _popupsContainer;

        public override void InstallBindings()
        {
            BindConfig();
            BindContainers();
            BindCharactersBase();
            BindStateMachine();
            BindStates();
            BindUIElements();
            BindTurn();
            BindControllers();
            BindRunner();
        }

        private void BindConfig()
        {
            Container
                .Bind<GameConfigLoader>()
                .FromNew()
                .AsSingle();
        }

        private void BindTurn()
        {
            Container
                .Bind<TurnSequence>()
                .FromNew()
                .AsSingle();
        }

        private void BindUIElements()
        {
            Container
                .Bind<CharacterSelectionPopupLoader>()
                .FromNew()
                .AsSingle();
        }

        private void BindControllers()
        {
            Container
                .Bind<StateUpdateController>()
                .AsSingle();

            Container
                .Bind<CharacterSelectionController>()
                .AsSingle();
        }

        private void BindStates()
        {
            Container
                .Bind<IState>()
                .To<SelectCharacterState>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<IState>()
                .To<StartGameState>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<IState>()
                .To<RollTheDiceState>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<IState>()
                .To<TurnState>()
                .FromNew()
                .AsSingle();
        }

        private void BindStateMachine()
        {
            Container
                .Bind(typeof(IStateSwitcher), typeof(FiniteStateMachine))
                .To<FiniteStateMachine>()
                .FromNew()
                .AsSingle();
        }

        private void BindRunner()
        {
            Container
                .BindInterfacesAndSelfTo<GameRunner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCharactersBase()
        {
            Container
                .Bind<CharactersBase>()
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