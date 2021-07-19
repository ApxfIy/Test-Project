using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.GameData;
using Assets.Scripts.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameTargets[] _gameTargets;
        [SerializeField] private DifficultySettings _difficultySettings;
        [SerializeField] private GameGrid _gameGrid;
        [SerializeField] private GameUI _gameUi;

        [field: SerializeField] public UnityEvent OnCorrectAnswer { get; private set; } = new UnityEvent();

        private GameTarget _answer;
        private IResettable[] _resettables;
        private GameTarget[] _currentShuffledTargets;
        private List<GameTarget> _currentOriginalTargets;
        private List<List<GameTarget>> _targets;

        private void Awake()
        {
            Initialize();
            RefreshItemsList();
            ResetAllComponents();
            SetUpLevel();
        }

        private void OnDestroy()
        {
            _gameUi.OnRestart.RemoveListener(RestartGame);
            _gameGrid.OnCellClick -= GameGridOnCellClick;
            StopAllCoroutines();
        }

        private void Initialize()
        {
            _resettables = FindObjectsOfType<MonoBehaviour>().OfType<IResettable>().ToArray();
            _targets = new List<List<GameTarget>>(_gameTargets.Length);
            for (var i = 0; i < _targets.Capacity; i++)
            {
                _targets.Add(new List<GameTarget>());
            }
            _gameGrid.OnCellClick += GameGridOnCellClick;
            _gameUi.OnRestart.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            StartCoroutine(RestartGameRoutine());
        }

        private IEnumerator RestartGameRoutine()
        {
            _gameUi.ShowLoading();
            _gameUi.ShowEndPanel(false);

            yield return null;
            
            ResetAllComponents();

            _gameUi.ShowLoading(false);

            SetUpLevel();
        }

        private void GameGridOnCellClick(int index)
        {
            if (_currentShuffledTargets[index] != _answer) return;

            _currentOriginalTargets.Remove(_currentShuffledTargets[index]);

            if (_currentOriginalTargets.Count == 0)
                _targets.Remove(_currentOriginalTargets);

            OnCorrectAnswer?.Invoke();

            if (_difficultySettings.Increase())
                SetUpLevel();
            else
                _gameUi.ShowEndPanel();
        }

        private void SetUpLevel()
        {
            _currentOriginalTargets = PickTargets();

            var difficulty = _difficultySettings.CurrentData;
            
            if (difficulty > _currentOriginalTargets.Count)
                Debug.LogWarning($"Targets doesn't contain enough elements for this difficulty ({_currentOriginalTargets.Count} < {difficulty})");

            _currentShuffledTargets = _currentOriginalTargets.Shuffle().Take(difficulty).ToArray();

            _answer = _currentShuffledTargets[Random.Range(0, _currentShuffledTargets.Length)];
            _gameGrid.SetData(_currentShuffledTargets);
            _gameUi.SetTargetName(_answer.Name);
        }

        private List<GameTarget> PickTargets()
        {
            if (_gameTargets == null || _gameTargets.Length == 0)
                throw new Exception("Game targets aren't assigned.");

            if (_targets.Count == 0)
            {
                Debug.LogWarning("All targets was already found at least once. Resetting targets state");
                RefreshItemsList();
            }

            return _targets[Random.Range(0, _targets.Count)];
        }

        private void RefreshItemsList()
        {
            _targets.ForEach(t=>t.Clear());

            for (var i = 0; i < _targets.Capacity; i++)
            {
                _targets[i].AddRange(_gameTargets[i].Targets);
            }
        }

        private void ResetAllComponents()
        {
            _difficultySettings.ResetSettings(); 

            foreach (var resettable in _resettables)
            {
                resettable.ResetComponent();
            }
        }
    }
}