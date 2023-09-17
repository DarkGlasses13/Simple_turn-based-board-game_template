﻿using System.Collections.Generic;

namespace Assets._Project.Turn_Sequencing
{
    public class Turn
    {
        private readonly GameConfig _config;

        private readonly List<Player> _players = new();
        private int _currentPlayerIndex;

        public int PlayersCount => _players.Count;
        public bool CanRegistNewPlayers => _players.Count < _config.PlayersLimit;
        public Player CurrentPlayer => _players[_currentPlayerIndex];

        public Turn(GameConfigLoader configLoader)
        {
            _config = configLoader.Load();
        }

        public bool TryRegistNewPlayer(string name, string characterID)
        {
            if (_currentPlayerIndex >= _config.PlayersLimit)
            {
                Complete();
                return false;
            }

            _players.Add(new(_currentPlayerIndex, name, characterID));
            _currentPlayerIndex++;
            return true;
        }

        public void Complete()
        {
            _currentPlayerIndex = 0;
        }

        public void Next()
        {
            _currentPlayerIndex = _currentPlayerIndex >= _players.Count 
                ? 0
                : _currentPlayerIndex + 1;
        }

        public void ForgetPlayers()
        {
            _players.Clear();
            Complete();
        }
    }
}
