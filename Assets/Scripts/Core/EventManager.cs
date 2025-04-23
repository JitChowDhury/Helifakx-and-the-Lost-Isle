using UnityEngine;
using UnityEngine.Events;
namespace RPG.Core
{

    public static class EventManager
    {
        public static event UnityAction OnChangePlayerHealth;
        public static void RaiseChangePlayerHealth() => OnChangePlayerHealth?.Invoke();//null cond operator
    }

}


