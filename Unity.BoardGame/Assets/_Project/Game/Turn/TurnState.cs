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
        private readonly DiceController _diceController;
        private readonly Way _way;
        private readonly CharactersBase _characters;

        public TurnState(IStateSwitcher switcher, TurnSequence turn,
            DiceController diceController, Way way, CharactersBase characters) : base(switcher)
        {
            _turn = turn;
            _diceController = diceController;
            _way = way;
            _characters = characters;
        }

        public override void Enter()
        {
            Debug.Log(_turn.CurrentPlayer.Name + "'s turn");
            _diceController.Enable();
            _diceController.OnRolled += OnDiceRolled;
        }

        private void OnDiceRolled(int result)
        {
            _diceController.OnRolled -= OnDiceRolled;
            _diceController.Disable();
            IEnumerable<IWaypoint> way = _way.Get(_turn.CurrentPlayer, result);
            IWaypoint lastWaypoint = way.ElementAt(way.Count() - 1);
            _way.Enter(_turn.CurrentPlayer, lastWaypoint.Index, out bool isFinished);
            _characters
                .GetByID(_turn.CurrentPlayer.CharacterID, isUsed: true)
                .Move(lastWaypoint.CharactersContainer);

            if (isFinished)
            {
                _switcher.Switch<FinishGameState>();
            }
            else
            {
                _turn.Next();
                _switcher.Switch<TurnState>();
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
