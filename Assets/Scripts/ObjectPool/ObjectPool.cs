using System;
using System.Collections.Generic;

namespace ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private readonly int _preloadCount;

        private Queue<T> _pool = new Queue<T>();

        public ObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            if (preloadFunc == null)
            {
                throw new NullReferenceException("Preload function is null");
            }

            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;
            _preloadCount = preloadCount;
        }
        
        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _pool.Enqueue(item);
        }
        
        protected void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++) Return(_preloadFunc());
        }
    }
}