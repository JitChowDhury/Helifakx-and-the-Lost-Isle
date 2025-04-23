using UnityEngine;
using UnityEngine.Events;
namespace RPG.Core
{

    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static void RaiseChangePlayerHealth(float newHealthPoints) => OnChangePlayerHealth?.Invoke(newHealthPoints);//null cond operator
    }

}


