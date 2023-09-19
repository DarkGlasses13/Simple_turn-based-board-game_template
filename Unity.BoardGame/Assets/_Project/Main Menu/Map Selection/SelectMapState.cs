using Finite_State_Machine;

namespace Assets._Project.Main_Menu.Map_Selection
{
    public class SelectMapState : State
    {
        private readonly MapSelectionController _mapSelectionController;

        public SelectMapState(IStateSwitcher switcher, MapSelectionController mapSelectionController) : base(switcher)
        {
            _mapSelectionController = mapSelectionController;
        }

        public override void Enter()
        {
            _mapSelectionController?.Enable();
        }

        public override void Exit()
        {
            _mapSelectionController?.Disable();
        }

        public override void Update() { }
    }
}
