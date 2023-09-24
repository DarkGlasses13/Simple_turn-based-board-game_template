using Assets._Project.Game.Board;
using Assets.Package.Tokens.Actors;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Game.Characters
{
    public class Character : Actor
    {
        private readonly GameConfig _config;

        public Character(CharacterData data, CharacterInstance instance, GameConfig config) : base(data, instance)
        {
            _config = config;
        }

        public void Move(IEnumerable<Waypoint> way, Action onMotionended = null)
        {
            // TODO: remove sequence kill

            Sequence motion = DOTween.Sequence();
            motion.SetAutoKill(true);
            Instance.transform.SetParent(null);

            for (int i = 0; i < way.Count(); i++)
            {
                Vector3 waypointPosition = way.ElementAt(i).CharactersContainer.transform.position + Vector3.up;
                Vector3 rotation;

                if (i == 0)
                {
                    rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - Instance.transform.position).eulerAngles.y;
                }
                else
                {
                    rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - way.ElementAt(i - 1).CharactersContainer.position).eulerAngles.y;
                }

                motion.Append(Instance.transform
                    .DORotate(rotation, _config.TurnRotationDuration)).Append(Instance.transform
                    .DOMove(waypointPosition, _config.TurnStepMotionDuration));
            }

            motion.Play().OnComplete(() =>
            {
                Instance.transform.SetParent(way.ElementAt(way.Count() - 1).CharactersContainer);
                onMotionended?.Invoke();
            });
        }

        public void Move(Waypoint destination, Action onMotionended = null)
        {
            // TODO: remove sequence kill

            Sequence motion = DOTween.Sequence();
            motion.SetAutoKill(true);
            Instance.transform.SetParent(null);
            Vector3 waypointPosition = destination.CharactersContainer.transform.position + Vector3.up;
            Vector3 rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - Instance.transform.position).eulerAngles.y;

            motion.Append(Instance.transform
                    .DORotate(rotation, _config.TurnRotationDuration)).Append(Instance.transform
                    .DOMove(waypointPosition, _config.TurnStepMotionDuration));

            motion.Play().OnComplete(() =>
            {
                Instance.transform.SetParent(destination.CharactersContainer);
                onMotionended?.Invoke();
            });
        }
    }
}
