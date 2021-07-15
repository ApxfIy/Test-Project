using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RestartPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [field: SerializeField] public UnityEvent OnShow { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent OnHide { get; private set; } = new UnityEvent();

        public UnityEvent OnRestart => _button?.onClick;

        public void Show(bool show = true)
        {
            gameObject.SetActive(show);

            if (show)
                OnShow?.Invoke();
            else
                OnHide?.Invoke();
        }
    }
}