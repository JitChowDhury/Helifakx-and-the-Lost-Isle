using UnityEngine;
using RPG.Character;

public class AIReturnState : AIBaseState
{
    private Vector3 targetPosition;
    public override void EnterState(EnemyController enemy)
    {
        enemy.movementCMP.isRunning = false;
        enemy.movementCMP.UpdateAgentSpeed(enemy.stats.walkSpeed);
        if (enemy.patrolCmp != null)
        {
            targetPosition = enemy.patrolCmp.GetNextPosition();
            enemy.movementCMP.MoveAgentByDestination(targetPosition);

        }
        else
        {
            enemy.movementCMP.MoveAgentByDestination(enemy.originalPosition);

        }
    }

    public override void UpdateState(EnemyController enemy)
    {
        if (enemy.distanceFromPlayer < enemy.chaseRange)
        {
            enemy.SwitchState(enemy.chaseState);
            return;
        }



        if (enemy.movementCMP.ReachedDestination())
        {

            if (enemy.patrolCmp != null)
            {
                enemy.SwitchState(enemy.patrolState);
                return;
            }
            else
            {
                enemy.movementCMP.Rotate(enemy.movementCMP.originalForwardVector);
                enemy.movementCMP.isMoving = false;
            }
        }
        else
        {
            if (enemy.patrolCmp != null)
            {
                Vector3 lookTowardsVector = targetPosition - enemy.transform.position;
                lookTowardsVector.y = 0;
                enemy.movementCMP.Rotate(lookTowardsVector);
            }
            else
            {
                Debug.Log(enemy.originalPosition);
                Vector3 lookTowardsVector = enemy.originalPosition - enemy.transform.position;
                lookTowardsVector.y = 0;
                enemy.movementCMP.Rotate(lookTowardsVector);
            }
        }




    }
}
