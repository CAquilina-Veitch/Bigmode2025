using R3;
using UnityEngine;

namespace Extensions
{
    public class DisposableBehaviour : MonoBehaviour
    {
        public readonly CompositeDisposable DisposableBehaviourCompositeDisposable = new();
        private void OnDestroy() => DisposableBehaviourCompositeDisposable.Dispose();
    }
}