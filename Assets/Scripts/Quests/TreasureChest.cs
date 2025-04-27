using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;
namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        public Animator animator;
        private Animator playerAnimator;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;
        [SerializeField] private QuestItemSO questItem;

        void Awake()
        {
            playerAnimator = GameObject.FindWithTag("Player").GetComponentInChildren<Animator>();
        }
        void OnTriggerEnter(Collider other)
        {
            print("Player detected");
            isInteractable = true;
        }
        void OnTriggerExit(Collider other)
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened || !context.performed) return;

            EventManager.RaiseTresureChestUnlocked(questItem);
            playerAnimator.SetTrigger(Constants.INTERACT_ANIMATOR_PARAM);
            animator.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            hasBeenOpened = true;


        }
    }

}

