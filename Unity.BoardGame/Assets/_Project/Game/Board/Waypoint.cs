using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Game.Board
{
    public class Waypoint : MonoBehaviour, IWaypoint
    {
        private readonly List<Player> _players = new();

        [field: SerializeField] public Transform CharactersContainer { get; private set; }
        public IReadOnlyCollection<Player> Characters => _players.AsReadOnly();

        public int Index => transform.GetSiblingIndex();

        public bool Contains(Player player) => _players.Contains(player);

        public void Enter(Player player)
        {
            _players.Add(player);
        }

        public void Exit(Player player)
        {
            _players.Remove(player);
        }
    }
}
