using System;
using UnityEngine;

namespace Assets._Project.Game.Board
{
    public interface IWaypoint
    {
        int Index { get; }
        Transform CharactersContainer { get; }
        bool Contains(Player player);
        void Enter(Player player);
        void DoAction(Player player, Action onPerformed = null);
        void Exit(Player player);
    }
}