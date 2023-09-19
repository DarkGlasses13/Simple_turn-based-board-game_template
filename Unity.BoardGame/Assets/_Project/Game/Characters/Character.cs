using Assets._Project.Actors_Base;
using UnityEngine;

namespace Assets._Project.Game.Characters
{
    public class Character : Actor
    {
        public Character(CharacterData data) : base(data)
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
