using System;
using Audio;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unit = R3.Unit;

namespace Extensions
{
    public static class ReactiveExtensions
    {
        public static Observable<Unit> OnClickAsObservable(this Button button)
        {
            return Observable.FromEvent(
                h => button.onClick.AddListener(h.Invoke),
                h => button.onClick.RemoveListener(h.Invoke)
            );
        }
        public static void AssignTo(this IDisposable disposable, SerialDisposable serialDisposable) => serialDisposable.Disposable = disposable;

        public static IDisposable Subscribe<T>(this Observable<T> source, Action action)
            => source.Subscribe(_ => action.Invoke());
        public static T AddTo<T>(this T disposable, DisposableBehaviour disposables) where T : IDisposable
        {
            disposables.DisposableBehaviourCompositeDisposable.Add(disposable);
            return disposable;
        }

        public static RectTransform rectTransform(this Component component) => (RectTransform)component.transform;
    }
}