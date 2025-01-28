using System.Collections.Generic;
using Extensions;
using R3;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private BossMovementDriver movementDriver;

        [SerializeField] private List<BossPhase> Phases;
        private int currentBossPhase;
        private int currentBossPhaseStep;

        private BossAttackData currentBossAttack;
        
        private readonly SerialDisposable phaseStepInstructionDisposable = new();
        private readonly SerialDisposable healthValueNextPhaseDisposable = new();

        public void Start()
        {
            CheckPhaseProgress();
        }

        private void CheckPhaseProgress()
        {
            if (healthValueNextPhaseDisposable == null)
                CurrentHealth
                    .Where(newHealth => newHealth <= Phases[currentBossPhase].GoToNextPhaseWhenHealthIsLessThanThisValue && newHealth > 0)
                    .Subscribe(NextPhaseOnHealthMeetsThreshold).AssignTo(healthValueNextPhaseDisposable);
            ChooseAttackFromCurrentPhase();
        }

        private void NextPhaseOnHealthMeetsThreshold()
        {
            currentBossAttack.Attack.ClearAttackDisposable();
            currentBossPhase++;
            if (currentBossPhase >= Phases.Count)
                Debug.LogError($"{currentBossPhase} is greater than num of phases.", this);
            

        }
        private void ChooseAttackFromCurrentPhase()
        {
            var currentPhaseStep = Phases[currentBossPhase].PhaseSteps[currentBossPhaseStep];
            currentBossAttack = currentPhaseStep.GetRandomBossAttackData();
            currentBossAttack.Attack.OnAttackFinished
                .Subscribe(_ => OnAttackFinished(currentBossAttack.phaseStepInstruction))
                .AssignTo(phaseStepInstructionDisposable);
            currentBossAttack.Attack.Attack();
            
            
            
        }

        private void OnAttackFinished(int phaseStepInstruction)
        {
            CompletePhaseStepInstruction(phaseStepInstruction);
            phaseStepInstructionDisposable.Disposable = null;
            CheckPhaseProgress();
        }
        public void CompletePhaseStepInstruction(int instruction)
        {
            currentBossPhaseStep += instruction;
            if (currentBossPhaseStep < 0) 
                currentBossPhaseStep = 0;

            if (currentBossPhaseStep >= Phases[currentBossPhase].PhaseSteps.Count)
            {
                Debug.LogWarning($"Found End of phase {currentBossPhase} steps {currentBossPhaseStep}, this shouldn't happen." , this );
                currentBossPhaseStep = 0;
                //NextBossPhase();
            }
        }

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
        
        
    }
}