using Architecture_Base.Core;
using Architecture_Base.Scene_Switching;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    public class ProjectRunner : Runner, ITickable, ILateTickable, IFixedTickable
    {
        private readonly ISceneSwitcher _sceneSwitcher;

        public ProjectRunner(ISceneSwitcher sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        protected override Task CreateControllers()
        {
            _controllers = new IController[] 
            {

            };

            return Task.CompletedTask;
        }

        protected override void OnControllersInitializedAndEnabled()
        {
            Application.targetFrameRate = 60;
            _sceneSwitcher.ChangeAsync("Demo Game");
        }
    }
}