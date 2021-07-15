using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class Particle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Play()
        {
            var instance = Instantiate(_particleSystem);
            instance.Play();
        }

    }
}