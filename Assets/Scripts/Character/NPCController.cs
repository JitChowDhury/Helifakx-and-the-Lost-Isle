
using System;
using Ink.Parsed;
using RPG.Core;
using RPG.Quest;
using RPG.UI;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Character
{

    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;
        private Reward rewardCmp;
        public TextAsset inkJSON;

        public QuestItemSO questItem;
        public bool hasQuestItem = false;

        void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
            rewardCmp = GetComponent<Reward>();
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
                return;
            }
            EventManager.RaiseInitiateDialogue(inkJSON, gameObject);


        }
        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem) return true;

            Inventory inventoryCmp = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<Inventory>();
            hasQuestItem = inventoryCmp.HasItem(questItem);

            if (rewardCmp != null && hasQuestItem)
            {
                rewardCmp.SendReward();
            }
            return hasQuestItem;
        }
    }
}