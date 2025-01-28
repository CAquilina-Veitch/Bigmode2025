using System;
using System.Collections.Generic;
using Extensions;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private BossMovementDriver movementDriver;

        [SerializeField] private List<BossPhase> Phases;


        
        
        
        
        
    }

    public class BossHurtBox : MonoBehaviour
    {
        [SerializeField] private Collider hurtbox;

        private void Awake()
        {
            hurtbox.OnCollisionEnterAsObservable().Subscribe().AddTo(this);
        }

        public ReadOnlyReactiveProperty<Collision> CurrentCollision => currentCollision;
        private readonly ReactiveProperty<Collision> currentCollision = new();
    }
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private Collider[] hurtBoxes;
        
        private readonly ReactiveProperty<int> currentHealth;
        public ReadOnlyReactiveProperty<int> CurrentHealth => currentHealth;
        
        public void TakeDamage(int amount)
        {
            currentHealth.Value -= amount;
            if (currentHealth.CurrentValue <= 0)
            {
                
            }
            
        }

        public BossPhase[] phases;
        
        private BossPhase currentPhase;
        
        
        
        
    }

    [Serializable] public class BossPhaseStep
    {
        public List<BossAttackData> BossAttacks;
        public int nextStepOnComplete = 1;
    }

    [Serializable] public enum NextStepInstruction
    {
        NextStep,
        BackAStep,
        FirstStep
    }

    [Serializable] public class BossAttackData
    {
        public BossAttack Attack;
        public int phaseStepInstruction = 1;
    }
    [Serializable] public abstract class BossAttack : MonoBehaviour
    {
        public Observable<Unit> OnAttackFinished => onAttackFinished;
        private readonly Subject<Unit> onAttackFinished = new();

        public abstract void Attack();

        public void FinishAttack() => onAttackFinished.OnNext(Unit.Default);
    }
    
}