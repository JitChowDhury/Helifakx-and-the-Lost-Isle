using UnityEngine;

namespace RPG.Character
{
    public class AIDefeatedState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            AudioSource audioSourceCmp = enemy.GetComponent<AudioSource>();
            if (audioSourceCmp.clip == null) return;
            audioSourceCmp.Play();
        }

        public override void UpdateState(EnemyController enemy)
        {

        }
    }
}


