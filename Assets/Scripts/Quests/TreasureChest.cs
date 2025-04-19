using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        public Animator animator;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;


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

            animator.SetBool("isShaking", false);
            hasBeenOpened = true;


        }
    }

}

