using Assets._Project.Game.Board.Waypoint_Action_Strategies;
using UnityEngine;

namespace Assets._Project.Game.Board.Waypoints
{
    public class TransitWaypoint : Waypoint<TransitWaypointAction> 
    {
        [field: SerializeField] public int TransitDestinationIndex { get; private set; }
    }
}