using Assets.Package.Tokens;
using UnityEngine;

namespace Assets._Project.Game.Maps
{
    [CreateAssetMenu]
    public class MapData : TokenData, IDescriptable
    {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; internal set; }
    }
}
