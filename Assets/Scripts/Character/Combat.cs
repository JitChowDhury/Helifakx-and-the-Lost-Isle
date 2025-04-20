using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public bool isAttacking = false;
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
            bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEvent.OnBubbleEndAttack += HandleBubbleCompleteAttack;
            bubbleEvent.OnBubbleHit += HandleBubbleHit;


        }
        void OnDisable()
        {
            bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            bubbleEvent.OnBubbleEndAttack -= HandleBubbleCompleteAttack;
            bubbleEvent.OnBubbleHit -= HandleBubbleHit;

        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed && isAttacking) return;
            StartAttack();

        }

        private void StartAttack()
        {
            if (isAttacking) return;
            animator.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0);
            animator.SetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
        }
        private void HandleBubbleStartAttack()
        {
            isAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            isAttacking = false;
        }

        private void HandleBubbleHit()
        {
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward, transform.localScale / 2, Vector3.forward, transform.rotation, 1f
              );
            foreach (RaycastHit target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;
                Health healthCmp = target.transform.gameObject.GetComponent<Health>();
                if (healthCmp == null) continue;
                healthCmp.takeDamage(damage);
            }
        }

    }

}
