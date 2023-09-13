using Assets._Project.Gameplay_States;
using Finite_State_Machine;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameInstaller : ScriptableObjectInstaller 
    {
        public override void InstallBindings()
        {
            BindRunner();
            BindStateMachine();
            BindStates();
            BindControllers();
        }

        private void BindControllers()
        {
            Container
                .Bind<StateUpdateController>()
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
                .To<GetTurnOrderState>()
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
    }
}