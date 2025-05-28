using Services;
using UnityEngine;

namespace SoundsGame
{
    public abstract class SoundsPlayer : MonoBehaviour
    {
        private PauseService _pauseService;
        private AudioSource _audioSource;
        private AudioClip _audioClip;

        public AudioSource AudioSource => _audioSource;
        public AudioClip AudioClip => _audioClip;

        private void OnDisable()
        {
            _pauseService.PauseEnabled -= DisableVolume;
            _pauseService.PauseDisabled -= EnableVolume;
        }

        public void Construct(PauseService pauseService, AudioSource audioSource, AudioClip audioClip)
        {
            _pauseService = pauseService;
            _audioSource = audioSource;
            _audioClip = audioClip;

            _pauseService.PauseEnabled += DisableVolume;
            _pauseService.PauseDisabled += EnableVolume;
        }

        public abstract void PlaySound();

        public void EnableVolume()
        {
            _audioSource.volume = 1.0f;
        }

        public void DisableVolume()
        {
            _audioSource.volume = 0f;
        }
    }
}
