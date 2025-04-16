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
                enemy.SwitchState(enemy.chaseState);
                return;
            }
            UnityEngine.Debug.Log("Attacking");

        }



    }
}