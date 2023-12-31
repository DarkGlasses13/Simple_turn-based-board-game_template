﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Game.Board
{
    public class Way
    {
        private readonly List<Waypoint> _waypoints;

        public IWaypoint Start => _waypoints[0];
        public IWaypoint End => _waypoints[^1];

        public Way(List<Waypoint> waypoints)
        {
            _waypoints = waypoints;
        }

        public IEnumerable<IWaypoint> Get(Player player, int steps)
        {
            int direction = steps > 0 ? 1 : -1;
            Waypoint start = GetPlayersPoint(player);

            if (start)
            {
                List<IWaypoint> way = new();
                int iStart = start.Index;

                for (int i = iStart; Mathf.Abs(i) < Mathf.Abs(iStart + steps); i += direction)
                {
                    if (i + 1 >= _waypoints.Count)
                        return way;

                    way.Add(_waypoints[i + 1]);
                }

                return way;
            }

            return null;
        }

        private Waypoint GetPlayersPoint(Player player)
        {
            return _waypoints.SingleOrDefault(waypoint => waypoint.Contains(player));
        }

        public void Enter(Player player, int to, out bool isFinished)
        {
            Waypoint from = GetPlayersPoint(player);

            if (from != null)
                from.Exit(player);

            _waypoints[to].Enter(player);
            isFinished = to == End.Index;
        }
    }
}
