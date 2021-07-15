using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class SimpleFadeAnimation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _fadeOutValue;
        [SerializeField] private float _fadeInValue;

        public void FadeIn()
        {
            _canvasGroup.alpha = _fadeOutValue;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(_fadeInValue, _duration);
        }

        public void FadeOut()
        {
            _canvasGroup.alpha = _fadeInValue;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(_fadeOutValue, _duration);
        }
    }
}