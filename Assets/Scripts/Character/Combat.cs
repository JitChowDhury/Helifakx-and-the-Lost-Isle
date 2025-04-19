using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float damage = 0f;
        private Animator animator;
        private BubbleEvent bubbleEvent;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            bubbleEvent = GetComponentInChildren<BubbleEvent>();

        }
        void OnEnable()
        {
            bubbleEvent.OnBubbleStartAttack += HadleBubbleStartAttack;
            bubbleEvent.OnBubbleEndAttack += HandleBubbleCompleteAttack;


        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            StartAttack();
        }

        private void StartAttack()
        {
            animator.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0);
            animator.SetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
        }
        private void HadleBubbleStartAttack()
        {
            print("Start attack");
        }

        private void HandleBubbleCompleteAttack()
        {
            print("Complete attack");
        }
    }

}
