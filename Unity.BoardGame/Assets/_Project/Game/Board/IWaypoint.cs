using UnityEngine;

namespace Assets._Project.Game.Board
{
    public interface IWaypoint
    {
        int Index { get; }
        Transform CharactersContainer { get; }
    }
}