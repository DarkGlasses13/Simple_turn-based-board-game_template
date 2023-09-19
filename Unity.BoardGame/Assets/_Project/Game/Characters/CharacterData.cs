using Assets._Project.Actors_Base;
using UnityEngine;

namespace Assets._Project.Game.Characters
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject, IActorData
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; internal set; }
    }
}
