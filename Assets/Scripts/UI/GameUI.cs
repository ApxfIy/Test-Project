using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameUI : MonoBehaviour, IResettable
    {
        [SerializeField] private TMP_Text _targetName;
        [SerializeField] private RestartPanel _endPanel;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Button _restartButton;
        
        [field: SerializeField] public UnityEvent OnReset { get; private set; } = new UnityEvent();
        
        public UnityEvent OnRestart => _restartButton?.onClick;

        public void ResetComponent()
        {
            //Some Reset logic here

            OnReset?.Invoke();
        }

        public void SetTargetName(string n)
        {
            _targetName.text = $"Find {n}";
        }

        public void ShowEndPanel(bool show = true)
        {
            _endPanel.Show(show);
        }

        public void ShowLoading(bool show = true)
        {
            _loadingScreen.SetActive(show);
        }

        private void Awake()
        {
            ShowEndPanel(false);
            ShowLoading(false);
        }
    }
}