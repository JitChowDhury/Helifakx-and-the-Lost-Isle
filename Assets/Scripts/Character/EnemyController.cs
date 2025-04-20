using System;
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
        private Health healthCmp;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;
        public CharacterStatsSO stats;

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
        }

        void OnEnable()
        {
            healthCmp.OnStartDefeated += HandleStartDefeated;
        }
        void OnDisable()
        {
            healthCmp.OnStartDefeated -= HandleStartDefeated;
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



    }
}
