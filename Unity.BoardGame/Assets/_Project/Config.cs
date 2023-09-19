﻿using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        [field: SerializeField] public int PlayersLimit {  get; private set; }
        [field: SerializeField] public int DiceMaxValue { get; private set; }
    }
}
