using System;

namespace Assets._Project.Game.Board
{
    public interface IWaypointActionStrategy
    {
        void Performe(Player player, Action callback = null);
    }
}