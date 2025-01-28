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
        public static void AssignTo(this IDisposable disposable, SerialDisposable serialDisposable) => serialDisposable.Disposable = disposable;

        public static IDisposable Subscribe<T>(this Observable<T> source, Action action)
            => source.Subscribe(_ => action.Invoke());
        /*
        public static T AddTo<T>(this T disposable, MonoBehaviour disposables) where T : IDisposable
        {
            disposables.Add(disposable);
            return disposable;
        }
        */

        public static RectTransform rectTransform(this Component component) => (RectTransform)component.transform;
    }
}