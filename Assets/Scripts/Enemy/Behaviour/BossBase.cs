using System.Collections.Generic;
using Extensions;
using R3;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    public partial class BossBase : DisposableBehaviour
    {
        [SerializeField] private BossMovementDriver movementDriver;

        [SerializeField] private List<BossPhase> Phases;


        
        
        
        
        
    }

    public partial class BossBase : DisposableBehaviour
    {
        [SerializeField] private int maxHealth;
        private readonly ReactiveProperty<int> currentHealth;
        public ReadOnlyReactiveProperty<int> CurrentHealth => currentHealth;

        public void TakeDamage(int amount)
        {
            currentHealth.Value -= amount;
            if (currentHealth.CurrentValue <= 0)
            {
                
            }
            
        }


        public BossPhase currentPhase;
    }
    public class BossPhase : ScriptableObject
    {
        public int HealthRequirement;
    }

    public class BossAttack
    {
        public BaseBossCondition EndCondition;
        
    }

    public class BaseBossCondition
    {
        
    }

    public class DelayBossCondition
    {
        
    }
    
    public class TestingFrogBoss : BossBase
    {
        
    }
}