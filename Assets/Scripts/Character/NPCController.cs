
using System;
using Ink.Parsed;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Character
{

    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;
        public TextAsset inkJSON;

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
            if (inkJSON == null)
            {
                Debug.LogWarning("Please add an ink file to the npc.");
            }
            print("talking to NPC");

        }
    }
}