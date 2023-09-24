using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._Project.Game.Board
{
    public abstract class Waypoint<TActionStrategy> : MonoBehaviour, IWaypoint where TActionStrategy : IWaypointActionStrategy
    {
        protected Way _way;
        private TActionStrategy _actionStrategy;
        private readonly List<Player> _players = new();

        [field: SerializeField] public Transform CharactersContainer { get; private set; }
        public IReadOnlyCollection<Player> Characters => _players.AsReadOnly();
        public int Index => transform.GetSiblingIndex();

        // TODO: Create a non monobehaviour layer and use this as view

        [Inject]
        public void Construct(Way way, TActionStrategy actionStrategy)
        {
            _way = way;
            _actionStrategy = actionStrategy;
        }

        public bool Contains(Player player) => _players.Contains(player);

        public virtual void Enter(Player player)
        {
            _players.Add(player);
        }

        public void DoAction(Player player, Action onPerformed = null)
        {
            if (Contains(player))
            {
                _actionStrategy?.Performe(player, onPerformed);
            }
        }

        public virtual void Exit(Player player)
        {
            _players.Remove(player);
        }
    }
}
