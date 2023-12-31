﻿using Assets._Project.Game.Board;
using Assets._Project.Game.Character_Selection;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Dice_Rolling;
using Assets._Project.Game.Turn;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._Project.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCharactersBase();
            BindStates();
            BindUIElements();
            BindTurnSequence();
            BindWay();
            BindControllers();
            BindRunner();
        }

        private void BindWay()
        {
            Transform wayParent = FindObjectOfType<Waypoint>().transform.parent;

            Container
                .Bind<List<Waypoint>>()
                .FromInstance(wayParent.GetComponentsInChildren<Waypoint>().ToList());

            Container
                .Bind<Way>()
                .FromNew()
                .AsSingle();
        }

        private void BindTurnSequence()
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

            Container
                .Bind<DicePopupLoader>()
                .FromNew()
                .AsSingle();
        }

        private void BindControllers()
        {
            Container
                .Bind<CharacterSelectionController>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<DiceController>()
                .FromNew()
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
                .To<TurnState>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<IState>()
                .To<FinishGameState>()
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
    }
}