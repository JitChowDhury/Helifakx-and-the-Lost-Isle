using RPG.Character;
using UnityEngine;

namespace RPG.Quest
{
[CreateAssetMenu(
    fileName = "Reward",
    menuName ="RPG/Reward",
    order =2
)]
    public class RewardSO : ScriptableObject
    {
        public float bonusHealth = 0f;
        public float bondusDamage = 0f;
        public int bonusPotions = 0;
        public bool forceWeaponSwap = false;

        public Weapons weapons=Weapons.Sword;
    }
}