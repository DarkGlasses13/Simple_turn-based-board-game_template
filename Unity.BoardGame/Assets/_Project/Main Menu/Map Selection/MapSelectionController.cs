using Architecture_Base.Core;
using Architecture_Base.Scene_Switching;
using Assets._Project.Game.Character_Selection;
using Assets._Project.Main_Menu.Maps;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets._Project.Main_Menu.Map_Selection
{
    public class MapSelectionController : Controller
    {
        private readonly Transform _popupContainer;
        private readonly MapSelectionPopupLoader _mapSelectionPopupLoader;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ISceneSwitcher _sceneSwitcher;
        private List<MapData> _datas;
        private MapSelectionPopup _popup;

        public MapSelectionController([Inject(Id = "Popup")] Transform popupContainer,
            MapSelectionPopupLoader mapSelectionPopupLoader, IStateSwitcher stateSwitcher,
            ISceneSwitcher sceneSwitcher)
        {
            _popupContainer = popupContainer;
            _mapSelectionPopupLoader = mapSelectionPopupLoader;
            _stateSwitcher = stateSwitcher;
            _sceneSwitcher = sceneSwitcher;
        }

        public override async Task InitializeAsync()
        {
            _datas = new(await Addressables.LoadAssetsAsync<MapData>("Map Data", null).Task);
            _popup = await _mapSelectionPopupLoader.LoadAndInstantiateAsync(_popupContainer, isActive: false);
            await _popup.ConstructAsync(_datas);
        }

        protected override void OnEnable()
        {
            _popup.Show();
            _popup.OnSelect += OnSelect;
        }

        private void OnSelect(int index)
        {
            _sceneSwitcher.ChangeAsync(_datas[index].ID);
            _stateSwitcher.Switch<SelectCharacterState>();
        }

        protected override void OnDisable()
        {
            _popup.OnSelect -= OnSelect;
            _popup.Hide();
        }
    }
}
