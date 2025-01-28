using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Scripts.Enemy.Behaviour
{
    [Serializable] public class BossPhaseStep
    {
        public List<BossAttackData> BossAttacks;
        public int nextStepOnComplete = 1;
        public BossAttackData GetRandomBossAttackData() => BossAttacks[Random.Range(0, BossAttacks.Count)];
        
        
        
        
    }
}