using System.Collections.Generic;

namespace Amheklerior.Core.ObjectPooling {
    
    public class Pool<T> : AbstractPool<T> {

        private int _capacity;

        private HashSet<T> _managedInstances = new HashSet<T>();
        private Queue<T> _availableInstances = new Queue<T>();

        public Pool(CreationFunc<T> create, int capacity = 100, bool canExpand = false) : 
            base(create, capacity, canExpand) => _capacity = capacity;

        protected override int Capacity => _capacity;

        protected override bool IsEmpty => _availableInstances.Count == 0;

        protected override bool IsManaged(T instance) => _managedInstances.Contains(instance);

        protected override T GetFromPool() => _availableInstances.Dequeue();

        protected override void PutBackIntoPool(T instance) => _availableInstances.Enqueue(instance);

        protected override void AddToPool(T newInstance) {
            _managedInstances.Add(newInstance);
            _availableInstances.Enqueue(newInstance);
        }
        
        protected override void Expand() {
            base.Expand();
            _capacity *= 2;
        }

    }

    
    #region Debuggable version

    public class DebuggablePool<T> : Pool<T> {

        public DebuggablePool(CreationFunc<T> create, int capacity = 100, bool canExpand = false) :
            base(create, capacity, canExpand) { }


        protected override bool IsEmpty {
            get {
                var isEmpty = base.IsEmpty;
                if (isEmpty)
                    System.Diagnostics.Debug.Write($"The pool does not have any more instances to give.");
                return isEmpty;
            }
        }

        protected override bool IsManaged(T instance) {
            var isManaged = base.IsManaged(instance);
            System.Diagnostics.Debug.Write(
                $"The instance {instance} {(isManaged? "is" : "is not")} a managed instance. " +
                $"Hence, it {(isManaged ? "can " : "cannot")} be put back to the pool.");
            return isManaged;
        }

        protected override void AddToPool(T newInstance) {
            System.Diagnostics.Debug.Write($"The instance {newInstance} has been created and added to the pool.");
            base.AddToPool(newInstance);
        }

        protected override void Expand() {
            System.Diagnostics.Debug.Write($"The pool is going to expand.");
            base.Expand();
        }

        protected override T GetFromPool() {
            var instance = base.GetFromPool();
            System.Diagnostics.Debug.Write($"The instance {instance} is taken from the pool.");
            return instance;
        }

        protected override void PutBackIntoPool(T instance) {
            System.Diagnostics.Debug.Write($"The instance {instance} has been released and put back to the pool.");
            base.PutBackIntoPool(instance);
        }
    }

    #endregion
    
}
