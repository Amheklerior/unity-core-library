using System;
using UnityEngine;

namespace Amheklerior.Core.ObjectPooling {
    
    public class GameObjectPool : MonoBehaviour, IPool<GameObject> {

        #region Inspector interface

        [Header("Settings:"), Space]

        [Tooltip(tooltip:"The prototype prefab of the pooled objects.")]
        [SerializeField] private GameObject _prototype;

        [Tooltip(tooltip:"The maximum allowed amount of pooled object.")]
        [SerializeField, Range(10, 1000)] private int _poolCapacity = 100;
        
        [Tooltip(tooltip:"Determine if the pool is allowed to expand if the need of more instances arises.")]
        [SerializeField] private bool _allowExpansion = false;

        [Tooltip(tooltip:"Automatically sets the objects as active when used and inactive when released.")]
        [SerializeField] private bool _automanageObjectActivation = true;
        
        #endregion

        private IPool<GameObject> _objectPool;

        public Action<GameObject> OnGet {
            get => _objectPool.OnGet;
            set => _objectPool.OnGet = value;
        }
        public Action<GameObject> OnPut {
            get => _objectPool.OnPut;
            set => _objectPool.OnPut = value;
        }

        public GameObject Get() => _objectPool.Get();
        public void Put(GameObject instance) => _objectPool.Put(instance);


        #region Unity lifecycle
        
        private void Awake() {
            if (_prototype == null) {
                Debug.LogError("No prototype has been been found.", _prototype);
                throw new NullReferenceException();
            }
            _transform = transform;
        }

        private void Start() => InitializePool();

        #endregion

        #region Internals 

        protected Transform _transform;

        protected CreationFunc<GameObject> CreateFunc => Create;

        private GameObject Create() {
            GameObject instance = InstantiateFrom(_prototype);
            OnCreate?.Invoke(instance);
            return instance;
        }

        private void InitializePool() {
            var pool = GetPoolImplementation();
            if (_automanageObjectActivation) {
                _objectPool.OnGet += (GameObject instance) => instance.SetActive(true);
                _objectPool.OnPut += (GameObject instance) => instance.SetActive(false);
            }
        }

        protected Action<GameObject> OnCreate { get; set; } 
            = (GameObject instance) => instance.SetActive(false);

        protected virtual GameObject InstantiateFrom(GameObject prototype) 
            => Instantiate(prototype, Vector3.zero, Quaternion.identity, _transform);

        protected virtual IPool<GameObject> GetPoolImplementation() 
            => new Pool<GameObject>(CreateFunc, _poolCapacity, _allowExpansion);

        #endregion

    }
}


/*
 * Example of extending the ObjectPoolComponent class to provide specific implementation
 * 

internal class PoolCUSTOM<T> : IPool<T> {
    public Action<T> OnGet { get; set; }
    public Action<T> OnPut { get; set; }
    public T Get() {
        T instance = default;
        // your Get-logic here
        return instance;
    }
    public void Put(T instance) {
        // your Put-logic here
    }
}

public class GameObjectPoolCUSTOM : ObjectPoolComponent {
    protected override IPool<GameObject> GetPoolImplementation() => new PoolCUSTOM<GameObject>();
}

public class GameObjectPool_CUSTOM_OBJECT_CREATION : ObjectPoolComponent {
    protected override GameObject InstantiateFrom(GameObject prototype) => Instantiate(prototype);
}

*/
