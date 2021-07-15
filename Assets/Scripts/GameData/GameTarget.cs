using System;
using UnityEngine;

namespace Assets.Scripts.GameData
{
    [Serializable]
    public class GameTarget : IDisplayable
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}