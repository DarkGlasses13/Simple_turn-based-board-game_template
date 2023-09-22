using Assets.Package.Tokens;
using UnityEngine;

namespace Assets._Project.Game.Characters
{
    [CreateAssetMenu]
    public class CharacterData : TokenData, IDescriptable
    {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
