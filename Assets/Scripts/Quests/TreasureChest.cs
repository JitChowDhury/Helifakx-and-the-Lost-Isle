using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        public Animator animator;
        private Animator playerAnimator;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;

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
            if (!isInteractable || hasBeenOpened) return;

            animator.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            playerAnimator.SetTrigger(Constants.INTERACT_ANIMATOR_PARAM);
            hasBeenOpened = true;


        }
    }

}

