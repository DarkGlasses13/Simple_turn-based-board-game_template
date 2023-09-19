using Assets._Project.Game.Character_Selection;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Dice_Rolling;
using Assets._Project.Game.Turn_Sequencing;
using Finite_State_Machine;
using UnityEngine;
using Zenject;

namespace Assets._Project.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform
            _hudContainer,
            _popupsContainer;

        public override void InstallBindings()
        {
            BindContainers();
            BindCharactersBase();
            BindStates();
            BindUIElements();
            BindTurn();
            BindControllers();
            BindRunner();
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