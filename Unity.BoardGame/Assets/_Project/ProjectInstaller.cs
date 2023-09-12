using Architecture_Base.Scene_Switching;
using Assets._Project.Scene_Swith;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            BindSceneSwitcher();
            BindControllers();
            BindRunner();
        }

        private void BindRunner()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectRunner>()
                .FromNew()
                .AsSingle();
        }

        private void BindControllers()
        {

        }

        private void BindSceneSwitcher()
        {
            Container
                .Bind<ISceneSwitcher>()
                .To<SceneSwitcher>()
                .FromNew()
                .AsSingle();
        }
    }
}