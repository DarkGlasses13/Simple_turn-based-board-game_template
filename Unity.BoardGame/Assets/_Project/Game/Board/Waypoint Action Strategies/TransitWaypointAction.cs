using Assets._Project.Game.Characters;
using Assets._Project.Game.Turn;
using System;

namespace Assets._Project.Game.Board.Waypoint_Action_Strategies
{
    public class TransitWaypointAction : IWaypointActionStrategy
    {
        private readonly Way _way;
        private readonly TurnSequence _turn;
        private readonly CharactersBase _characters;

        public TransitWaypointAction(Way way, TurnSequence turn, CharactersBase characters)
        {
            _way = way;
            _turn = turn;
            _characters = characters;
        }

        public void Performe(Player player, Action callback = null)
        {
            IWaypoint destination = _way.GetWaypointByIndex(13);
            _way.Enter(player, 13, out bool isFinished);
            _characters.GetByID(player.CharacterID, isUsed: true).Move(destination, callback);
        }
    }
}
