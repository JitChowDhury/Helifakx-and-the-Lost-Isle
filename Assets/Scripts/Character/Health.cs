using System;
using RPG.Utility;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };
        [NonSerialized] public float healthPoints = 0f;
        private bool isDefeated = false;
        private BubbleEvent bubbleEventCmp;

        private Animator animatorCmp;

        void Awake()
        {
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            animatorCmp = GetComponentInChildren<Animator>();
        }

        void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteHit += HandleBubbleCompleteDefeat;

        }
        void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteHit -= HandleBubbleCompleteDefeat;
        }
        public void takeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);
            if (healthPoints == 0)
            {
                Defeated();

            }
        }

        private void Defeated()
        {
            if (isDefeated) return;
            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated.Invoke();
            }
            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }
        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }


    }



}
