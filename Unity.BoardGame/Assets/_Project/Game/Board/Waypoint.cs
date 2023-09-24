using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._Project.Game.Board
{
    public abstract class Waypoint : MonoBehaviour
    {
        protected Way _way;
        protected IWaypointActionStrategy _action;
        private readonly List<Player> _players = new();

        [field: SerializeField] public Transform CharactersContainer { get; private set; }
        public IReadOnlyCollection<Player> Characters => _players.AsReadOnly();
        public int Index => transform.GetSiblingIndex();

        [Inject]
        public void Construct(Way way)
        {
            _way = way;
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
                _action?.Performe(player, onPerformed);
            }
        }

        public virtual void Exit(Player player)
        {
            _players.Remove(player);
        }
    }
}
