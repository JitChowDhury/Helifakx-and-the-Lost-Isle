using UnityEngine;
using UnityEngine.Events;
using RPG.Quest;

namespace RPG.Core
{

    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePotionsCount;
        public static event UnityAction<TextAsset, GameObject> OnInitiateDialogue;
        public static event UnityAction<QuestItemSO> OnTreasureChestUnlocked;

        public static event UnityAction<bool> OnToggleUI;
        public static void RaiseChangePlayerHealth(float newHealthPoints) => OnChangePlayerHealth?.Invoke(newHealthPoints);//null cond operator
        public static void RaiseChangePotionCount(int newPotionCount) => OnChangePotionsCount?.Invoke(newPotionCount);
        public static void RaiseInitiateDialogue(TextAsset inkJSON,GameObject NPC) => OnInitiateDialogue?.Invoke(inkJSON,NPC);

        public static void RaiseTresureChestUnlocked(QuestItemSO item) => OnTreasureChestUnlocked?.Invoke(item);
        public static void RaiseToggleUI(bool isOpened) => OnToggleUI?.Invoke(isOpened);
    }

}


