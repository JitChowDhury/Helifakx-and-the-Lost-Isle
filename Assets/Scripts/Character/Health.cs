using System;
using RPG.Utility;
using Unity.Burst.CompilerServices;
using UnityEngine;
namespace RPG.Character
{
    public class Health : MonoBehaviour
    {

        [NonSerialized] public float healthPoints = 0f;
        private bool isDefeated = false;

        private Animator animatorCmp;

        void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
        }
        public void takeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);
            if (healthPoints == 0)
            {
                Defeated();

            }
        }

        private void Defeated()
        {
            if (isDefeated) return;
            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }
    }



}
