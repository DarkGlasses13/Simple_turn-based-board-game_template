using System.Collections.Generic;

namespace Assets._Project.Game.Turn
{
    public class TurnSequence
    {
        private readonly GameConfig _config;

        private readonly List<Player> _players = new();
        private int _currentPlayerIndex;

        public int PlayersCount => _players.Count;
        public bool CanRegistNewPlayers => _players.Count < _config.PlayersLimit;
        public Player CurrentPlayer => _players[_currentPlayerIndex];
        public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

        public TurnSequence(GameConfigLoader configLoader)
        {
            _config = configLoader.Load();
        }

        public bool TryRegistNewPlayer(string name, string characterID)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(characterID))
                return false;

            if (_currentPlayerIndex >= _config.PlayersLimit)
            {
                CompleteRegistration();
                return false;
            }

            _players.Add(new(_currentPlayerIndex, name, characterID));
            _currentPlayerIndex++;
            return true;
        }

        public void CompleteRegistration()
        {
            _currentPlayerIndex = 0;
        }

        public void Next()
        {
            _currentPlayerIndex = _currentPlayerIndex >= _players.Count - 1
                ? 0
                : _currentPlayerIndex + 1;
        }

        public void ForgetPlayers()
        {
            _players.Clear();
            CompleteRegistration();
        }
    }
}
