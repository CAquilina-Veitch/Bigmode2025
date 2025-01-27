using System;
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
    [Serializable] public class BossPhase
    {
        public int HealthRequirement;

        public List<BossPhaseStep> PhaseStep;





    }

    [Serializable] public class BossPhaseStep
    {
        public List<BossAttackData> BossAttacks;
        public int nextStepOnComplete;
    }

    [Serializable] public enum NextStepInstruction
    {
        NextStep,
        BackAStep,
        FirstStep
    }
    [Serializable] public class BossAttackData
    {
        [SerializeField] public BossAttack Attack;
        public NextStepInstruction NextStepInstructionOnComplete;
    }
    
    [Serializable] public abstract class BossAttack : MonoBehaviour
    {
        public Observable<Unit> OnAttackFinished => onAttackFinished;
        private readonly Subject<Unit> onAttackFinished = new();

        public abstract void Attack();

        public void FinishAttack() => onAttackFinished.OnNext(Unit.Default);
    }


    public class DelayBossCondition
    {
        
    }
    
    public class TestingFrogBoss : BossBase
    {
        
    }
    
}