using System;
using UnityEngine;
namespace RPG.Character
{
    public class Health : MonoBehaviour
    {

        [NonSerialized] public float healthPoints = 0f;
        public void takeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);
            print(healthPoints);
        }
    }
}
