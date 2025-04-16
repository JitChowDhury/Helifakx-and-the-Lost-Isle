using System;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character
{

    public class EnemyController : MonoBehaviour
    {
        private GameObject player;
        private Movement movement;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        [NonSerialized] public float distanceFromPlayer;
        void Awake()
        {
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movement = GetComponent<Movement>();
        }


        void Update()
        {
            CalculateDistanceToPlayer();
            chasePlayer();
        }

        private void chasePlayer()
        {
            if (distanceFromPlayer > chaseRange) return;
            movement.MoveAgentByDestination(player.transform.position);
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




    }
}
