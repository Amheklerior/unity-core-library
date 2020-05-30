using System;
using UnityEngine;
using Amheklerior.Core.Common;

namespace Amheklerior.Core.ObjectPooling {
    
    public class GameObjectPool : RichMonoBehaviour, IPool<GameObject> {

        #region Inspector interface

        [Space, Header("Settings:")]

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

        protected Action<GameObject> OnCreate { get; set; } = (GameObject instance) => instance.SetActive(false);

        protected virtual GameObject InstantiateFromPrototype(GameObject prototype) 
            => Instantiate(prototype, Vector3.zero, Quaternion.identity, _transform);

        protected virtual IPool<GameObject> GetPoolImplementation() {
#if UNITY_EDITOR
            _description = _description ?? $"GameObjectPool: {name}";
            return _debugMode
                ? new DebuggablePool<GameObject>(CreateFunc, _poolCapacity, _allowExpansion)
                : new Pool<GameObject>(CreateFunc, _poolCapacity, _allowExpansion);
#else
            return new Pool<GameObject>(CreateFunc, _poolCapacity, _allowExpansion);
#endif
        }

        private GameObject Create() {
            GameObject instance = InstantiateFromPrototype(_prototype);
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

        #endregion
		
    }
}
