using System;
using R3;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
    [Serializable] public class BossAttackData
    {
        public BossAttack Attack;
        public int phaseStepInstruction = 1;
    }
    [Serializable] public abstract class BossAttack : MonoBehaviour
    {
        protected BossBase bossBase;
        protected Transform bossTransform => bossBase.transform;
        protected Transform playerTransform;
        protected readonly CompositeDisposable isAttackingDisposable = new();
        public Observable<Unit> OnAttackFinished => onAttackFinished;
        private readonly Subject<Unit> onAttackFinished = new();

        public void DefineTransforms(BossBase boss, Transform player)
        {
            bossBase = boss;
            playerTransform = player;
        }
        public abstract void Attack();

        protected void FinishAttack()
        {
            Debug.Log("finished attack.");
            isAttackingDisposable.Clear();
            onAttackFinished.OnNext(Unit.Default);
        }

        public void ClearAttackDisposable() => 
            isAttackingDisposable.Clear();

        public void OnDestroy() => 
            ClearAttackDisposable();
    }
}