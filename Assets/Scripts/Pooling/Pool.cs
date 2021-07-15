using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pooling
{
    public class Pool<T>
    {
        private readonly Stack<T> _stack;
        private readonly Action<T> _push;
        private readonly Action<T> _pull;
        private readonly Func<T> _create;

        public Pool(int initialCapacity, Func<T> create, Action<T> onPush, Action<T> onPull)
        {
            _create = create ?? throw new ArgumentException("Create method is null. Can't initialize pool");
            initialCapacity = Mathf.Clamp(initialCapacity, 0, int.MaxValue);
            _push = onPush;
            _pull = onPull;

            _stack = new Stack<T>(initialCapacity);

            for (var i = 0; i < initialCapacity; i++)
            {
                Push(_create());
            }
        }

        public T Pull()
        {
            var instance = _stack.Count > 0 ? _stack.Pop() : _create();
            _pull?.Invoke(instance);
            return instance;
        }

        public void Push(T instance)
        {
            _push?.Invoke(instance);
            _stack.Push(instance);
        }
    }
}
