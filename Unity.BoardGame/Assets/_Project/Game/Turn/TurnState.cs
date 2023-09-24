using Assets._Project.Game.Board;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Dice_Rolling;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Game.Turn
{
    public class TurnState : State
    {
        private readonly TurnSequence _turn;
        private readonly Way _way;
        private readonly CharactersBase _characters;
        private readonly DiceController _diceController;
        private bool _isFinished;

        public TurnState(IStateSwitcher switcher, TurnSequence turn, Way way,
            CharactersBase characters, DiceController diceController) : base(switcher)
        {
            _turn = turn;
            _way = way;
            _characters = characters;
            _diceController = diceController;
        }

        public override void Enter()
        {
            Debug.Log(_turn.CurrentPlayer.Name + "'s turn");
            IEnumerable<IWaypoint> way = _way.Get(_turn.CurrentPlayer, _diceController.Result);
            _characters
                .GetByID(_turn.CurrentPlayer.CharacterID, isUsed: true)
                .Move(way, OnMotionEnded);
        }

        private void OnMotionEnded()
        {
            IEnumerable<IWaypoint> way = _way.Get(_turn.CurrentPlayer, _diceController.Result);
            IWaypoint lastWaypoint = way.ElementAt(way.Count() - 1);
            _way.Enter(_turn.CurrentPlayer, lastWaypoint.Index, out _isFinished);
            lastWaypoint.DoAction(_turn.CurrentPlayer, OnWaypointActionPerformed);
        }

        private void OnWaypointActionPerformed()
        {
            if (_isFinished)
            {
                _switcher.Switch<FinishGameState>();
            }
            else
            {
                _turn.Next();
                _switcher.Switch<RollTheDiceState>();
            }
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
