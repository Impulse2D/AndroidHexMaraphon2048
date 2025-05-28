using Services;
using UnityEngine;

namespace SoundsGame
{
    public class WinSoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private AudioClip _audioClip;

        public void Construct(PauseService pauseService, AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            _audioClip = audioClip;
        }

        public void PlaySound()
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
