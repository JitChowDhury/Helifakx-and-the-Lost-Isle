using UnityEngine;
using UnityEngine.Events;
namespace RPG.Core
{

    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePotionsCount;

        public static event UnityAction<TextAsset> OnInitiateDialogue;
        public static void RaiseChangePlayerHealth(float newHealthPoints) => OnChangePlayerHealth?.Invoke(newHealthPoints);//null cond operator
        public static void RaiseChangePotionCount(int newPotionCount) => OnChangePotionsCount?.Invoke(newPotionCount);
        public static void RaiseInitiateDialogue(TextAsset inkJSON) => OnInitiateDialogue?.Invoke(inkJSON);
    }

}


