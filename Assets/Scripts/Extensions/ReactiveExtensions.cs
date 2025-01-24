using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

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
        
    }
}