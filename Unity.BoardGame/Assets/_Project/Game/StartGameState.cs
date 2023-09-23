using Assets._Project.Game.Board;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Turn;
using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Game
{
    public class StartGameState : State
    {
        private readonly TurnSequence _turn;
        private readonly Way _way;
        private readonly CharactersBase _characters;

        public StartGameState(IStateSwitcher switcher, TurnSequence turn, Way way, CharactersBase characters) : base(switcher)
        {
            _turn = turn;
            _way = way;
            _characters = characters;
        }

        public override void Enter()
        {
            Debug.Log("Start game");
            
            foreach (Player player in _turn.Players)
            {
                _way.Enter(player, _way.Start.Index, out bool isFinished);
                _characters
                    .GetByID(player.CharacterID, isUsed: false, willWse: true)
                    .GetInstance().transform.SetParent(_way.Start.CharactersContainer);
            }

            _switcher.Switch<TurnState>();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
