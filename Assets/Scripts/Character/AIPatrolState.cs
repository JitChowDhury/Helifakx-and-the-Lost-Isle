using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Vector3 oldPosition = enemy.patrolCmp.GetNextPosition();
            enemy.patrolCmp.CalculateNextPosition();
            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPosition = enemy.patrolCmp.GetNextPosition();
            Vector3 offset = newPosition - currentPosition;


            enemy.movementCMP.MoveAgentByOffset(offset);

            Vector3 fartherOutPosition = enemy.patrolCmp.GetFartherDistance();
            Vector3 rotateTowardVector = fartherOutPosition - currentPosition;
            rotateTowardVector.y = 0;
            enemy.movementCMP.Rotate(rotateTowardVector);
            if (oldPosition == newPosition)
            {
                enemy.movementCMP.isMoving = false;
            }
        }


    }
}