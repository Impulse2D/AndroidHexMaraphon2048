using UnityEngine;

namespace SoundsGame
{
    public class CoinsSoundPlayer : SoundsPlayer
    {
        public override void PlaySound()
        {
            AudioSource.PlayOneShot(AudioClip);
        }
    }
}
