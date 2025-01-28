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
        protected Transform bossTransform;
        protected Transform playerTransform;
        protected readonly CompositeDisposable isAttackingDisposable = new();
        public Observable<Unit> OnAttackFinished => onAttackFinished;
        private readonly Subject<Unit> onAttackFinished = new();

        public void DefineTransforms(Transform boss, Transform player)
        {
            bossTransform = boss;
            playerTransform = player;
        }
        public abstract void Attack();

        protected void FinishAttack()
        {
            Debug.Log("finished attack.");
            isAttackingDisposable.Clear();
            onAttackFinished.OnNext(Unit.Default);
        }

        public void ClearAttackDisposable()
        {
            Debug.LogWarning("clearing attackdisposable");
            isAttackingDisposable.Clear();
        }
    }
}