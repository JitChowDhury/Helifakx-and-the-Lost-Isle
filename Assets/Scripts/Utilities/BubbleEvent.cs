using UnityEngine;
using UnityEngine.Events;
namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => { };//lambda expression
        public event UnityAction OnBubbleEndAttack = () => { };

        public event UnityAction OnBubbleHit = () => { };
        public event UnityAction OnBubbleCompleteHit = () => { };

        private void OnStartAttack()
        {
            OnBubbleStartAttack.Invoke();
        }

        private void OnEndAttack()
        {
            OnBubbleEndAttack.Invoke();
        }

        private void OnHit()
        {
            OnBubbleHit.Invoke();
        }

        private void OnCompleteDefeat()
        {
            OnBubbleCompleteHit.Invoke();
        }
    }
}
