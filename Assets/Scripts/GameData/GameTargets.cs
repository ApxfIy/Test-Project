using UnityEngine;

namespace Assets.Scripts.GameData
{
    [CreateAssetMenu(fileName = "GameTargets", menuName = "ScriptableObjects/GameTargets", order = 1)]
    public class GameTargets : ScriptableObject
    {
        public GameTarget[] Targets;
    }
}