using Architecture_Base.Scene_Switching;
using UnityEngine.AddressableAssets;

namespace Assets._Project.Scene_Swith
{
    public class SceneSwitcher : ISceneSwitcher
    {
        public async void ChangeAsync(string key)
        {
            await Addressables.LoadSceneAsync("Empty").Task;
            await Addressables.LoadSceneAsync(key).Task;
        }
    }
}