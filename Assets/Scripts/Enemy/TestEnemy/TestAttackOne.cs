using System;
using Extensions;
using R3;

namespace Scripts.Enemy.Behaviour
{
    public class TestAttackOne : BossAttack
    {
        public override void Attack()
        {
            Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(FinishAttack).AddTo(isAttackingDisposable);
        }
    }
}