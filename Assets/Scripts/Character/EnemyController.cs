using System;
using System.Collections.Generic;
using RPG.Core;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character
{

    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Movement movementCMP;
        [NonSerialized] public GameObject player;
        [NonSerialized] public Patrol patrolCmp;
        [NonSerialized] public Combat combatCmp;
        [NonSerialized] public bool hasUIopenend;
        private Health healthCmp;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;
        public CharacterStatsSO stats;
        public string enemyID = "";

        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();
        public AIPatrolState patrolState = new AIPatrolState();
        public AIDefeatedState defeatedState = new AIDefeatedState();

        void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }

            if (enemyID.Length == 0)
            {
                Debug.LogWarning($"{name} does not have a enemy ID");
            }

            originalPosition = transform.position;
            currentState = returnState;
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCMP = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

        }

        void Start()
        {
            currentState.EnterState(this);
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
            if (healthCmp.sliderCmp != null)
            {
                healthCmp.sliderCmp.maxValue = stats.health;
                healthCmp.sliderCmp.value = stats.health;
            }

            List<string> enemiesDefeated = PlayerPrefsUtility.GetString("EnemiesDefeated");
            enemiesDefeated.ForEach((ID) =>
            {
                if (ID == enemyID)
                {
                    Destroy(gameObject);
                }
            });
        }

        void OnEnable()
        {
            healthCmp.OnStartDefeated += HandleStartDefeated;
            EventManager.OnToggleUI += HandleToggleUI;
        }
        void OnDisable()
        {
            healthCmp.OnStartDefeated -= HandleStartDefeated;
            EventManager.OnToggleUI -= HandleToggleUI;
        }


        void Update()
        {
            CalculateDistanceToPlayer();


            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceToPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;
            //same as (enemyPosition-playerPosition).magnitude
            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

        }
        private void HandleStartDefeated()
        {
            SwitchState(defeatedState);
            currentState.EnterState(this);
        }

        private void HandleToggleUI(bool isOpened)
        {
            hasUIopenend = isOpened;
        }



    }
}
