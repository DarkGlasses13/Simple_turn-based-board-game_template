using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public int PlayersLimit {  get; private set; }
    }
}
