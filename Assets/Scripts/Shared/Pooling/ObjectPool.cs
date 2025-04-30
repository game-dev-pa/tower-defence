#nullable enable
using System;
using System.Collections.Generic;

namespace Shared.Pooling
{
    public sealed class ObjectPool<T> where T : class
    {
        private readonly int _maxPoolSize;
        private readonly Func<T> _createFunctionCall;
        private readonly Stack<T> _poolStack = new Stack<T>();

        private readonly Action<T>? OnGetFromPoolCallback;
        private readonly Action<T>? OnReturnToPoolCallback;

        public ObjectPool(
            Func<T> createFunctionCall,
            int initialPoolCapacity = 10,
            int maxPoolSize = 50,
            Action<T>? onGetFromPoolCallback = null,
            Action<T>? onReturnToPoolCallback = null)
        {
            _createFunctionCall = createFunctionCall ?? throw new ArgumentNullException(nameof(createFunctionCall));
            OnGetFromPoolCallback = onGetFromPoolCallback;
            OnReturnToPoolCallback = onReturnToPoolCallback;
            _maxPoolSize = maxPoolSize;

            for (var i = 0; i < initialPoolCapacity; i++)
                _poolStack.Push(_createFunctionCall());
        }

        public T GetFromPool()
        {
            var pooledItem = _poolStack.Count > 0 ? _poolStack.Pop() : _createFunctionCall();
            OnGetFromPoolCallback?.Invoke(pooledItem);
            (pooledItem as ICanPool)?.OnGetFromPool();
            return pooledItem;
        }

        public void ReturnToPool(T pooledItem)
        {
            if (_poolStack.Count >= _maxPoolSize) return;

            OnReturnToPoolCallback?.Invoke(pooledItem);
            (pooledItem as ICanPool)?.OnReturnToPool();
            _poolStack.Push(pooledItem);
        }
    }
}