using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    public abstract class GameEventListener : MonoBehaviour, IEventListener {
        
        [Tooltip(tooltip:"The event this listener is interested in.")]
        [SerializeField] protected GameEvent _gameEvent;
        
        public abstract void OnEventRaised();

        protected virtual void Awake() {
            if (!_gameEvent) {
                Debug.LogError("No game event has been set in the inspector. ", this);
                _gameEvent = ScriptableObject.CreateInstance<GameEvent>();
            }
        }

        protected virtual void OnEnable() => _gameEvent.Subscribe(OnEventRaised);
        protected virtual void OnDisable() => _gameEvent.Unsubscribe(OnEventRaised);
    }
    
    public abstract class GameEventListener<TEventData> : MonoBehaviour, IEventListener<TEventData> {
        
        [Tooltip(tooltip:"The event this listener is interested in.")]
        [SerializeField] protected GameEvent<TEventData> _gameEvent;
        
        public abstract void OnEventRaised(TEventData data);

        protected virtual void Awake() {
            if (!_gameEvent) {
                Debug.LogError("No game event has been set in the inspector. ", this);
                _gameEvent = ScriptableObject.CreateInstance<GameEvent<TEventData>>();
            }
        }

        protected virtual void OnEnable() => _gameEvent.Subscribe(OnEventRaised);
        protected virtual void OnDisable() => _gameEvent.Unsubscribe(OnEventRaised);
    }
    
}
