using System.Collections.Generic;
using Extensions;
using R3;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private List<BossPhase> Phases;
        [SerializeField] private Transform playerTransform;
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
            Debug.LogWarning("boss health met threshold, now going to phase " + (currentBossPhase + 1));
            currentBossAttack.Attack.ClearAttackDisposable();
            currentBossPhase++;
            if (currentBossPhase >= Phases.Count)
                Debug.LogError($"{currentBossPhase} is greater than num of phases.", this);
            healthValueNextPhaseDisposable.Disposable = null;

        }
        private void ChooseAttackFromCurrentPhase()
        {
            var currentPhaseStep = Phases[currentBossPhase].PhaseSteps[currentBossPhaseStep];
            Debug.Log($"getting attack from phase {currentBossPhase}, step {currentBossPhaseStep}");
            
            currentBossAttack = currentPhaseStep.GetRandomBossAttackData();
            
            Debug.Log($"Got Random Attack {currentBossAttack.Attack}");
            
            currentBossAttack.Attack.OnAttackFinished
                .Subscribe(_ => OnAttackFinished(currentBossAttack.phaseStepInstruction))
                .AssignTo(phaseStepInstructionDisposable);
            
            currentBossAttack.Attack.DefineTransforms(transform, playerTransform);
            currentBossAttack.Attack.Attack();
        }

        private void OnAttackFinished(int phaseStepInstruction)
        {
            phaseStepInstructionDisposable.Disposable = null;
            CompletePhaseStepInstruction(phaseStepInstruction);
            
        }
        public void CompletePhaseStepInstruction(int instruction)
        {
            Debug.Log($"instruction step + {instruction}, attempting {currentBossPhaseStep + instruction}");
            
            currentBossPhaseStep += instruction;
            if (currentBossPhaseStep < 0) 
                currentBossPhaseStep = 0;

            if (currentBossPhaseStep >= Phases[currentBossPhase].PhaseSteps.Count)
            {
                Debug.LogWarning($"Found End of phase {currentBossPhase} steps {currentBossPhaseStep}, this shouldn't happen." , this );
                currentBossPhaseStep = 0;
                //NextBossPhase();
            }
            CheckPhaseProgress();
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