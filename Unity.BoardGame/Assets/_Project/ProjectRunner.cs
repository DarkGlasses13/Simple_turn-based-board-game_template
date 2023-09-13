using Architecture_Base.Core;
using Architecture_Base.Scene_Switching;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    public class ProjectRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable
    {
        private readonly ISceneSwitcher _sceneSwitcher;

        public ProjectRunner(ISceneSwitcher sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        public void Initialize() => RunAsync();

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