using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public int PlayersLimit {  get; private set; }
        [field: SerializeField] public int DiceMaxValue { get; private set; }
        [field: SerializeField] public float TurnStepMotionDuration { get; private set; }
        [field: SerializeField] public float TurnRotationDuration { get; private set; }
    }
}
