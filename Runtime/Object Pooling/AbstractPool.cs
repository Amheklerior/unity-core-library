using System;

namespace Amheklerior.Core.ObjectPooling {

    public delegate T CreationFunc<T>();
    
    public abstract class AbstractPool<T> : IPool<T> {

        public Action<T> OnGet { get; set; }
        public Action<T> OnPut { get; set; }

        public AbstractPool(CreationFunc<T> create, int capacity = 100, bool canExpand = false) {
            if (capacity <= 0)
                throw new ArgumentException($"The pool capacity must be greater than zero. Was {capacity} instead");

            _canExpand = canExpand;
            _create = create;
            Fill(capacity);
        }

        public T Get() {
            if (IsEmpty && _canExpand) Expand();
            if (IsEmpty) return default;

            T instance = GetFromPool();
            OnGet?.Invoke(instance);
            return instance;
        }

        public void Put(T instance) {
            if (!IsManaged(instance)) return;

            OnPut?.Invoke(instance);
            PutBackIntoPool(instance);
        }

        
        #region Internals 

        private readonly CreationFunc<T> _create;
        private readonly bool _canExpand;

        protected virtual void Fill(int howMany) {
            T newInstance;
            for (int i = 0; i < howMany; i++) {
                newInstance = _create();
                AddToPool(newInstance);
            }
        }

        protected virtual void Expand() => Fill(Capacity);

        protected virtual int Capacity { get; }
        protected virtual bool IsEmpty { get; }
        protected abstract bool IsManaged(T instance);
        protected abstract void AddToPool(T newInstance);
        protected abstract T GetFromPool();
        protected abstract void PutBackIntoPool(T instance);

        #endregion

    }

}
