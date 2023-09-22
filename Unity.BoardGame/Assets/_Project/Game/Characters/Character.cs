using Architecture_Base.Asset_Loading;
using Assets.Package.Tokens.Actors;
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

        public void Move(Transform destination)
        {
            GetInstance(Data.ID).transform.SetParent(destination);
        }
    }
}
