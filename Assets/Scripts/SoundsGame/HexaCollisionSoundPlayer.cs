using UnityEngine;

namespace SoundsGame
{
    public class HexaCollisionSoundPlayer : SoundsPlayer
    {
        public override void PlaySound()
        {
            AudioSource.PlayOneShot(AudioClip);
        }
    }
}
