using Assets._Project.Game.Board.Waypoint_Action_Strategies;
using Zenject;

namespace Assets._Project.Game.Board.Waypoints
{
    public class SimpleWaypoint : Waypoint
    {
        [Inject]
        public void Construct(SimpleWaypointAction action)
        {
            _action = action;
        }
    }
}