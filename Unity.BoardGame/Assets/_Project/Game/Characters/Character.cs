using Architecture_Base.Asset_Loading;
using Assets._Project.Game.Board;
using Assets.Package.Tokens.Actors;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Game.Characters
{
    public class Character : Actor
    {
        public Character(CharacterData data, IInstanceLoader instanceLoader) : base(data, instanceLoader)
        {
        }

        protected override void OnInstanceLoaded(GameObject instance)
        {
            instance
                .AddComponent<CharacterInstance>()
                .Construct(Data.ID);
        }

        public void Move(IEnumerable<IWaypoint> way)
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

                //Vector3 rotation = Vector3.up * Quaternion.LookRotation(direction).eulerAngles.y;

                motion.Append(GetInstance().transform
                    //.DORotate(rotation, 0.5f)).Append(GetInstance().transform
                    .DOMove(waypointPosition, 0.5f));
            }

            motion
                .Play().OnComplete(() => GetInstance().transform
                .SetParent(way.ElementAt(way.Count() - 1).CharactersContainer));
        }
    }
}
