using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class DisposablesContainer
    {
        private readonly static List<IDisposable> _disposables = new();
        public static IEnumerable<IDisposable> Disposables => _disposables;

        public static void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public static void Clear()
        {
            _disposables.Clear();
        }
    }
}
