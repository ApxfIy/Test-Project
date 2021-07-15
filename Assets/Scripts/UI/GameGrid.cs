using System;
using System.Collections.Generic;
using Assets.Scripts.GameData;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Pooling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameGrid : MonoBehaviour, IResettable
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private Cell _cellPrefab;
        
        [field: SerializeField] public UnityEvent OnReset { get; private set; } = new UnityEvent();

        public event Action<int> OnCellClick;
        private Pool<Cell> _pool;
        private List<Cell> _activeCells;

        public void ResetComponent()
        {
            //Some Reset logic here

            OnReset?.Invoke();
        }

        public void SetData<T>(IList<T> data) where T : IDisplayable
        {
            if (_activeCells != null)
            {
                while (_activeCells.Count > data.Count)
                {
                    var index = _activeCells.Count - 1;
                    var c = _activeCells[index];
                    _activeCells.RemoveAt(index);
                    _pool.Push(c);
                }

                while (_activeCells.Count < data.Count)
                {
                    _activeCells.Add(_pool.Pull());
                }

                for (var i = 0; i < _activeCells.Count; i++)
                {
                    _activeCells[i].Initialize(data[i].Icon, i);
                }
            }
            else
            {
                _activeCells = new List<Cell>(data.Count);

                for (var i = 0; i < data.Count; i++)
                {
                    var cell = _pool.Pull();
                    cell.Initialize(data[i].Icon, i);
                    _activeCells.Add(cell);
                }
            }
        }

        private void Awake()
        {
            _pool = new Pool<Cell>(9, 
                onPull: (c) =>
                {
                    c.transform.SetParent(_gridLayout.transform);
                    c.transform.localScale = Vector3.one;
                    c.gameObject.SetActive(true);
                    c.OnClick += OnCellClick;
                }, 
                onPush: (c) =>
                {
                    c.gameObject.SetActive(false);
                    c.OnClick -= OnCellClick;
                }, 
                create: () => Instantiate(_cellPrefab)
                );
        }
    }
}