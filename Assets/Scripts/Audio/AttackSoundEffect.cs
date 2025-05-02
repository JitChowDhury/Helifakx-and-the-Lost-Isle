using UnityEngine;
namespace RPG.Audio
{
    public class AttackSoundEffect : MonoBehaviour
    {
        AudioSource audioSourceCmp;
        void Awake()
        {
            audioSourceCmp = GetComponent<AudioSource>();
        }

        private void OnStartAttack()
        {
            if (audioSourceCmp.clip == null) return;
            audioSourceCmp.Play();
        }
    }
}

