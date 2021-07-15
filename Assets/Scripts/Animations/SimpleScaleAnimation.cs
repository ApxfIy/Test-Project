using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class SimpleScaleAnimation : MonoBehaviour
    {
        [SerializeField] private ScaleSequence[] _scaleSequences;

        private Sequence _sequence;

        [ContextMenu("Play")]
        public void Play()
        {
            _sequence.Restart();
            _sequence.Play();
        }

        public void Stop()
        {
            _sequence.Pause();
        }

        private void Awake()
        {
            _sequence = CreateAnimationSequence();
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }

        private Sequence CreateAnimationSequence()
        {
            var sequence = DOTween.Sequence();
            foreach (var scaleSequence in _scaleSequences)
            {
                sequence.Append(transform.DOScale(scaleSequence.EndValue, scaleSequence.Duration)
                    .SetEase(scaleSequence.Ease));
            }

            sequence.Pause().SetAutoKill(false);
            return sequence;
        }

        [Serializable]
        private class ScaleSequence
        {
            public float Duration;
            public Vector3 EndValue;
            public Ease Ease;
        }
    }
}