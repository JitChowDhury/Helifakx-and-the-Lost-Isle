
using System;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Character
{

    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;

        void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                canvasCmp.enabled = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                canvasCmp.enabled = false;
            }
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled) return;

            print("talking to NPC");

        }
    }
}