using Architecture_Base.Asset_Loading;
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

        public Character(CharacterData data, IInstanceLoader instanceLoader, GameConfig config) : base(data, instanceLoader)
        {
            _config = config;
        }

        protected override void OnInstanceLoaded(GameObject instance)
        {
            instance
                .AddComponent<CharacterInstance>()
                .Construct(Data.ID);
        }

        public void Move(IEnumerable<IWaypoint> way, Action onMotionended = null)
        {
            // TODO: remove sequence kill

            Sequence motion = DOTween.Sequence();
            motion.SetAutoKill(true);
            GetInstance().transform.SetParent(null);

            for (int i = 0; i < way.Count(); i++)
            {
                Vector3 waypointPosition = new
                (
                    way.ElementAt(i).CharactersContainer.transform.position.x,
                    GetInstance().transform.position.y,
                    way.ElementAt(i).CharactersContainer.transform.position.z
                );

                Vector3 rotation;

                if (i == 0)
                {
                    rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - GetInstance().transform.position).eulerAngles.y;
                }
                else
                {
                    rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - way.ElementAt(i - 1).CharactersContainer.position).eulerAngles.y;
                }

                motion.Append(GetInstance().transform
                    .DORotate(rotation, _config.TurnRotationDuration)).Append(GetInstance().transform
                    .DOMove(waypointPosition, _config.TurnStepMotionDuration));
            }

            motion.Play().OnComplete(() =>
            {
                GetInstance().transform.SetParent(way.ElementAt(way.Count() - 1).CharactersContainer);
                onMotionended?.Invoke();
            });
        }

        public void Move(IWaypoint destination, Action onMotionended = null)
        {
            // TODO: remove sequence kill

            Sequence motion = DOTween.Sequence();
            motion.SetAutoKill(true);
            GetInstance().transform.SetParent(null);
            Vector3 waypointPosition = new
            (
                destination.CharactersContainer.transform.position.x,
                GetInstance().transform.position.y,
                destination.CharactersContainer.transform.position.z
            );

            Vector3 rotation = Vector3.up * Quaternion
                        .LookRotation(waypointPosition - GetInstance().transform.position).eulerAngles.y;

            motion.Append(GetInstance().transform
                    .DORotate(rotation, _config.TurnRotationDuration)).Append(GetInstance().transform
                    .DOMove(waypointPosition, _config.TurnStepMotionDuration));

            motion.Play().OnComplete(() =>
            {
                GetInstance().transform.SetParent(destination.CharactersContainer);
                onMotionended?.Invoke();
            });
        }
    }
}
