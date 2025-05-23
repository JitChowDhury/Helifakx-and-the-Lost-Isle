using RPG.Core;
using UnityEngine;
namespace RPG.Quest
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private RewardSO reward;

        bool rewardTaken = false;


        public void SendReward()
        {
            if (rewardTaken) return;
            EventManager.RaiseReward(reward);
            AudioSource audioSourceCmp = GetComponent<AudioSource>();
            if (audioSourceCmp == null) return;
            audioSourceCmp.Play();
            rewardTaken = true;
        }
    }
}
