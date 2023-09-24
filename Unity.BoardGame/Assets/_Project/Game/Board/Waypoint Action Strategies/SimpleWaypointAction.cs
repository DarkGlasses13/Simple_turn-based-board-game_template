using System;

namespace Assets._Project.Game.Board.Waypoint_Action_Strategies
{
    public class SimpleWaypointAction : IWaypointActionStrategy
    {
        public void Performe(Player player, Action callback = null) 
        {
            callback?.Invoke();
        }
    }
}
