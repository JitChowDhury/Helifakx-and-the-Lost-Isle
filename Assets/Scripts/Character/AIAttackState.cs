namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCMP.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {

            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.combatCmp.CancelAttack();
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            enemy.combatCmp.StartAttack();
            enemy.transform.LookAt(enemy.player.transform);

        }



    }
}