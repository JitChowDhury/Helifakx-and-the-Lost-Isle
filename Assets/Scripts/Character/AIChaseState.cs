using System.Diagnostics;

namespace RPG.Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            UnityEngine.Debug.Log("started chasing");
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
        }
    }
}