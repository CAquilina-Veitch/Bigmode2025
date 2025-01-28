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
        protected readonly CompositeDisposable isAttackingDisposable = new();
        public Observable<Unit> OnAttackFinished => onAttackFinished;
        private readonly Subject<Unit> onAttackFinished = new();

        public abstract void Attack();

        protected void FinishAttack()
        {
            onAttackFinished.OnNext(Unit.Default);
            isAttackingDisposable.Clear();
        }

        public void ClearAttackDisposable() => isAttackingDisposable.Clear();
    }
}