using RPG.Character;
using UnityEngine;
namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
        }
        void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;

        }
        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            PlayerController playerControllerCmp = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat("Health", playerControllerCmp.healthCmp.healthPoints);
            PlayerPrefs.SetInt("Potions", playerControllerCmp.healthCmp.potionCount);
            PlayerPrefs.SetFloat("Damage", playerControllerCmp.combatCmp.damage);
            PlayerPrefs.SetInt("Weapon", (int)playerControllerCmp.weapon);
            PlayerPrefs.SetInt("SceneIndex", nextSceneIndex);
        }
    }
}
