using System;
using System.Collections.Generic;
using RPG.Character;
using RPG.Utility;
using UnityEngine;
namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        private List<string> sceneEnemyIDs = new List<string>();
        private List<GameObject> enemiesAlive = new List<GameObject>();
        void Start()
        {
            List<GameObject> sceneEnemies = new List<GameObject>();
            sceneEnemies.AddRange(GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG));

            sceneEnemies.ForEach((GameObject sceneEnemy) =>
            {
                EnemyController enemyControllerCmp = sceneEnemy.GetComponent<EnemyController>();
                sceneEnemyIDs.Add(enemyControllerCmp.enemyID);

            });



        }

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

            enemiesAlive.AddRange(GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG));
            sceneEnemyIDs.ForEach(SaveDefeatedEnemies);
        }

        private void SaveDefeatedEnemies(string ID)
        {
            bool isAlive = false;
            enemiesAlive.ForEach((enemy) =>
            {
                string enemyID = enemy.GetComponent<EnemyController>().enemyID;
                if (enemyID == ID)
                {
                    isAlive = true;
                }
            });

            if (isAlive) return;
            List<string> enemiesDefeated = PlayerPrefsUtility.GetString("EnemiesDefeated");
            enemiesDefeated.Add(ID);

            PlayerPrefsUtility.SetString("EnemiesDefeated", enemiesDefeated);
        }

    }
}
