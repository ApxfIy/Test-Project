using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [field: SerializeField] public UnityEvent OnInitialize { get; private set; } = new UnityEvent();

        public event Action<int> OnClick;
        public int Index { get; private set; } = -1;

        public void Initialize(Sprite icon, int index)
        {
            _image.sprite = icon;
            Index = index;
            OnInitialize?.Invoke();
        }

        private void Awake()
        {
            _button.onClick.AddListener(InvokeOnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(InvokeOnClick);
        }

        private void InvokeOnClick()
        {
            OnClick?.Invoke(Index);
        }

    }
}