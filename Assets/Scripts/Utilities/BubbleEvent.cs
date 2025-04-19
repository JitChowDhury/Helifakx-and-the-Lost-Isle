using UnityEngine;
using UnityEngine.Events;
namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public UnityAction OnBubbleStartAttack = () => { };//lambda expression
        public UnityAction OnBubbleEndAttack = () => { };

        private void OnStartAttack()
        {
            OnBubbleStartAttack.Invoke();
        }

        private void OnEndAttack()
        {
            OnBubbleEndAttack.Invoke();
        }
    }
}
