using UnityEngine;

namespace RPG.Character
{
    public class AIDefeatedState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Defeated State Entered");
        }

        public override void UpdateState(EnemyController enemy)
        {

        }
    }
}


