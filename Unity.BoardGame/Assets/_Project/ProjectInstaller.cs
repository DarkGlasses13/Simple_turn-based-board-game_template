using Architecture_Base.Scene_Switching;
using Assets._Project.Scene_Swith;
using Finite_State_Machine;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindSceneSwitcher();
            BindConfig();
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

        private void BindRunner()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectRunner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindControllers()
        {
            Container
                .Bind<StateUpdateController>()
                .AsSingle();
        }

        private void BindSceneSwitcher()
        {
            Container
                .Bind<ISceneSwitcher>()
                .To<SceneSwitcher>()
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
    }
}