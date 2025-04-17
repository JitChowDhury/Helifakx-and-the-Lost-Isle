using UnityEngine;
using RPG.Character;

public class AIReturnState : AIBaseState
{
    private Vector3 targetPosition;
    public override void EnterState(EnemyController enemy)
    {
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

        if (enemy.patrolCmp != null)
        {
            if (enemy.movementCMP.ReachedDestination())
            {
                enemy.SwitchState(enemy.patrolState);
                return;
            }
        }
    }
}
