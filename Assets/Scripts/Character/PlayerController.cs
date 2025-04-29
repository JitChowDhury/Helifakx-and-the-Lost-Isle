using System;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
namespace RPG.Character
{

    public class PlayerController : MonoBehaviour
    {

        public CharacterStatsSO stats;
        [NonSerialized] public Health healthCmp;
        [NonSerialized] public Combat combatCmp;
        private GameObject axeWeapon;
        private GameObject swordWeapon;

        public Weapons weapon = Weapons.Axe;


        void OnEnable()
        {
            EventManager.OnReward += HandleReward;
        }

        void OnDisable()
        {
            EventManager.OnReward -= HandleReward;

        }

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
            axeWeapon = GameObject.FindGameObjectWithTag(Constants.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Constants.SWORD_TAG);
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("Health"))
            {
                healthCmp.healthPoints = PlayerPrefs.GetFloat("Health");
                healthCmp.potionCount = PlayerPrefs.GetInt("Potions");
                combatCmp.damage = PlayerPrefs.GetFloat("Damage");
                weapon = (Weapons)PlayerPrefs.GetInt("Weapon");
            }
            else
            {
                healthCmp.healthPoints = stats.health;
                combatCmp.damage = stats.damage;
            }


            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            SetWeapon();

        }

        private void HandleReward(RewardSO reward)
        {
            healthCmp.healthPoints += reward.bonusHealth;
            healthCmp.potionCount += reward.bonusPotions;
            combatCmp.damage += reward.bondusDamage;
            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePotionCount(healthCmp.potionCount);

            if (reward.forceWeaponSwap)
            {
                weapon = reward.weapons;
                SetWeapon();
            }
        }

        private void SetWeapon()
        {
            if (weapon == Weapons.Axe)
            {
                axeWeapon.SetActive(true);
                swordWeapon.SetActive(false);
            }
            else
            {
                axeWeapon.SetActive(false);
                swordWeapon.SetActive(true);
            }
        }
    }
}
