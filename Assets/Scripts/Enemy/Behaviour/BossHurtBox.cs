using R3;
using R3.Triggers;
using UnityEngine;

namespace Scripts.Enemy.Behaviour
{
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
}