using UnityEngine;
using UnityEngine.Events;
namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public UnityAction OnBubbleStartAttack;
        public UnityAction OnBubbleEndAttack;

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
