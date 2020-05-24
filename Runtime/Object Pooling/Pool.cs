using System;
using System.Collections.Generic;

namespace Amheklerior.Core.ObjectPooling {

    public delegate T CreationFunc<T>();

    /**
     * GOAL: Maximize operational speed performances
     * 
     * With fixed capacity: 
     *      Get -> O(1) 
     *      Release -> O(1)
     * 
     * Expandable:
     *      Get -> O(1), with worse-case scenario of O(n)
     *      Release -> O(1)
     *      
     * Memory footprint:
     *      An HashSet to track all instances managed by the pool
     *      A Queue to track all the ready-to-use instances
     * 
     * 
     * How to use: 
     *      At initialization the pool will be fill at full capacity.
     *          By default, the pool has fixed capacity of 100 slots. 
     *          You can specify a different capacity and/or its ability to expand upon needs in the constructor only.
     *      Use get to get and use an instance from the pool
     *      Use put to put it back to the pool for future usage
     *      
     *      
     * If you want to know more about Object Pooling in general, [Game Programming Patterns] is a brilliant source that covers the topic.
     */
    public class Pool<T> : IPool<T> {
        
        // SHOULD THEY BE SET ONLY?
        public Action<T> OnGet { get; set; }
        public Action<T> OnPut { get; set; }

        public Pool(CreationFunc<T> create, int capacity = 100, bool canExpand = false) {
            if (capacity <= 0)
                throw new ArgumentException("Pool capacity must be greater than zero.");

            _canExpand = canExpand;
            _capacity = capacity;
            _create = create;
            Fill(capacity);
        }

        public T Get() {
            if (IsEmpty && _canExpand) Expand();
            if (IsEmpty) return default;

            T instance = _availableInstances.Dequeue();
            OnGet?.Invoke(instance);
            return instance;
        }

        public void Put(T instance) {
            if (!IsManaged(instance)) return; 

            OnPut?.Invoke(instance);
            _availableInstances.Enqueue(instance);
        }


        #region Internals 

        private readonly CreationFunc<T> _create;
        private readonly bool _canExpand;
        
        private int _capacity;
        private HashSet<T> _managedInstances = new HashSet<T>();
        private Queue<T> _availableInstances = new Queue<T>();

        private bool IsEmpty => _availableInstances.Count == 0;
        private bool IsManaged(T instance) => _managedInstances.Contains(instance);
        
        private void Fill(int howMany) {
            T newInstance;
            for (int i = 0; i < howMany; i++) {
                newInstance = _create();
                _managedInstances.Add(newInstance);
                _availableInstances.Enqueue(newInstance);
            }
        }

        private void Expand() {
            if (!IsEmpty || !_canExpand) return;

            Fill(_capacity);
            _capacity *= 2;
        }
        
        #endregion
        
    }
}
