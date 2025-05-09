#nullable enable
using System;
using System.Collections.Generic;

namespace Shared.Pooling
{
    public sealed class ObjectPool<T> where T : ICanPool
    {
        private readonly int _maxPoolSize;
        private readonly Func<T> _createFunctionCall;
        private readonly Stack<T> _poolStack = new Stack<T>();
        
        public ObjectPool(
            Func<T> createFunctionCall,
            int initialPoolCapacity = 10,
            int maxPoolSize = 50)
        {
            _createFunctionCall = createFunctionCall ?? throw new ArgumentNullException(nameof(createFunctionCall));
            
            _maxPoolSize = maxPoolSize;

            for (var i = 0; i < initialPoolCapacity; i++)
                _poolStack.Push(_createFunctionCall());
        }

        public T GetFromPool()
        {
            var pooledItem = _poolStack.Count > 0 ? _poolStack.Pop() : _createFunctionCall();
            pooledItem.OnGetFromPool();
            return pooledItem;
        }

        public void ReturnToPool(T pooledItem)
        {
            if (_poolStack.Count >= _maxPoolSize) return;

            pooledItem.OnReturnToPool();
            _poolStack.Push(pooledItem);
        }
    }
}