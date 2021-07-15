using System;
using UnityEngine;

namespace Assets.Scripts.GameData
{
    [Serializable]
    public class IntDifficultyLevel
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int AssociatedData { get; private set; }
    }

    [CreateAssetMenu(fileName = "DifficultySettings", menuName = "ScriptableObjects/DifficultySettings", order = 2)]
    public class DifficultySettings : ScriptableObject
    {
        [SerializeField] private IntDifficultyLevel[] _levels;

        public int CurrentData => _levels[CurrentLevel].AssociatedData;
        public int CurrentLevel { get; private set; }

        public bool Increase()
        {
            if (CurrentLevel == _levels.Length - 1)
                return false;

            CurrentLevel++;
            return true;
        }

        public bool Decrease()
        {
            if (CurrentLevel == 0)
                return false;

            CurrentLevel--;
            return true;
        }

        public void ResetSettings()
        {
            CurrentLevel = 0;
        }

        private void Awake()
        {
            CurrentLevel = 0;
        }


    }
}