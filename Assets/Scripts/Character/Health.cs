using System;
using RPG.Core;
using RPG.Utility;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };
        public int potionCount = 1;
        [NonSerialized] public float healthPoints = 0f;
        [SerializeField] private float healAmount = 15f;
        [SerializeField] public Slider sliderCmp;
        public bool isDefeated = false;
        private BubbleEvent bubbleEventCmp;
        private Animator animatorCmp;



        void Awake()
        {
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            animatorCmp = GetComponentInChildren<Animator>();
            sliderCmp = GetComponentInChildren<Slider>();
        }

        void Start()
        {
            if (CompareTag(Constants.PLAYER_TAG))
                EventManager.RaiseChangePotionCount(potionCount);
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
            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerHealth(healthPoints);
            }
            if (sliderCmp != null)
            {
                sliderCmp.value = healthPoints;
            }
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

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0) return;

            potionCount--;
            EventManager.RaiseChangePotionCount(potionCount);
            healthPoints += healAmount;
            EventManager.RaiseChangePlayerHealth(healthPoints);
        }



    }



}
