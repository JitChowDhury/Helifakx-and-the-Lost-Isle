namespace RPG.Character
{
    public abstract class AIBaseState
    {
        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
    }

}
