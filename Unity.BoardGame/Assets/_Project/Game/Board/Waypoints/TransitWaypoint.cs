using Assets._Project.Game.Board.Waypoint_Action_Strategies;
using UnityEngine;
using Zenject;

namespace Assets._Project.Game.Board.Waypoints
{
    public class TransitWaypoint : Waypoint
    {
        [field: SerializeField] public int TransitDestinationIndex { get; private set; }

        [Inject]
        public void Construct(TransitWaypointAction action)
        {
            _action = action;
        }

        private void OnDrawGizmos()
        {
            if (TransitDestinationIndex != transform.GetSiblingIndex())
            {
                Gizmos.color = Color.green;
                Vector3 from = transform.position + Vector3.up;
                Vector3 to = transform.parent.GetChild(TransitDestinationIndex).transform.position + Vector3.up;
                Gizmos.DrawLine(from, to);
            }
        }
    }
}