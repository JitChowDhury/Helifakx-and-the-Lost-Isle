using System.Diagnostics;
using UnityEngine;

namespace RPG.Character
{
    public class AIChaseState : AIBaseState
    {

        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCMP.UpdateAgentSpeed(enemy.stats.runSpeed);
            UnityEngine.Debug.Log("started chasing");
            enemy.movementCMP.isRunning = true;
        }

        public override void UpdateState(EnemyController enemy)
        {

            if (enemy.distanceFromPlayer > enemy.chaseRange)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }

            if (enemy.distanceFromPlayer < enemy.attackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }
            enemy.movementCMP.MoveAgentByDestination(enemy.player.transform.position);

            Vector3 playerDirection = enemy.player.transform.position - enemy.transform.position;
            enemy.movementCMP.Rotate(playerDirection);
        }
    }
}