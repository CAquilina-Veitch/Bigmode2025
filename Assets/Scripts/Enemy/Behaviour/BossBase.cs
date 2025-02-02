using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Extensions;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private List<BossPhase> Phases;
        [SerializeField] private Transform playerTransform;
        public Animator animator;
        public SoundEffectPlayer soundEffectPlayer;
        private int currentBossPhase;
        private int currentBossPhaseStep;

        private BossAttackData currentBossAttack;
        
        private readonly SerialDisposable phaseStepInstructionDisposable = new();
        private readonly SerialDisposable healthValueNextPhaseDisposable = new();

        private string playerTag = Extensions.Extensions.GameTag(GameTags.PlayerAttack);
        private void Awake()
        {
            foreach (var atk in 
                     from phase in Phases 
                     from step in phase.PhaseSteps 
                     from atk in step.BossAttacks 
                     select atk)
                atk.Attack.DefineTransforms(this, playerTransform);
            currentHealth.Value = maxHealth;
        }

        public void Start()
        {
            CheckPhaseProgress();
            SubscribeToHurtboxes();
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
            
            currentBossAttack = currentPhaseStep.GetRandomBossAttackData();
            
            Debug.Log($"getting attack from phase {currentBossPhase}, step {currentBossPhaseStep}, now doing: {currentBossAttack.Attack}");
            
            currentBossAttack.Attack.OnAttackFinished
                .Subscribe(_ => OnAttackFinished(currentBossAttack.phaseStepInstruction))
                .AssignTo(phaseStepInstructionDisposable);
            
            currentBossAttack.Attack.Attack();
        }

        private void OnAttackFinished(int phaseStepInstruction)
        {
            phaseStepInstructionDisposable.Disposable = null;
            CompletePhaseStepInstruction(phaseStepInstruction);
            
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
            CheckPhaseProgress();
        }

    }

    
    
    
    
    
    
    
    
    
    public partial class BossBase : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private Collider2D[] hurtBoxes;
        private bool isOnHitCooldown;
        private float hitCooldownTime = 0.25f;

        private readonly ReactiveProperty<int> currentHealth = new();
        public ReadOnlyReactiveProperty<int> CurrentHealth => currentHealth;
        
        
        public void TakeDamage(int amount)
        {
            currentHealth.Value -= amount;
            isOnHitCooldown = true;
            Observable.Timer(TimeSpan.FromSeconds(hitCooldownTime)).Subscribe(_ => isOnHitCooldown = false).AddTo(this);
            if (currentHealth.CurrentValue <= 0) 
                Debug.LogWarning("Boss died;");
            
        }

        private void SubscribeToHurtboxes()
        {
            foreach (var hurtBox in hurtBoxes)
            {
                hurtBox.OnTriggerStay2DAsObservable().Subscribe(OnTriggerStay).AddTo(this);
            }
        }

        private void OnTriggerStay(Collider2D collision2D)
        {
            if (isOnHitCooldown) return;
            
            if (collision2D.CompareTag(playerTag)) 
                OnHit();
        }

        private void OnHit()
        {
            soundEffectPlayer.PlaySoundEffect(SoundEffectType.GetHit);
            TakeDamage(1);
        }

    }
}