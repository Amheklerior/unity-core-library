using UnityEngine;

namespace Amheklerior.Core.EventSystem {

    [RequireComponent(typeof(Collider))]
    public class GameEventTrigger : MonoBehaviour {

        protected enum TriggerStrategy {
            RAISE_ON_ENTER,
            RAISE_ON_STAY,
            RAISE_ON_EXIT
        }
        
        [Tooltip(tooltip:"The event to trigger.")]
        [SerializeField] protected GameEvent _gameEvent;

        [Tooltip(tooltip:"The strategy that defines when the event will be fired.")]
        [SerializeField] protected TriggerStrategy _strategy = TriggerStrategy.RAISE_ON_ENTER;
        
        protected virtual void Awake() {
            if (!_gameEvent) {
                Debug.LogError("No game event has been set in the inspector. ", this);
                _gameEvent = ScriptableObject.CreateInstance<GameEvent>();
            }
        }
        
        protected virtual void OnTriggerEnter() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_ENTER);
        protected virtual void OnTriggerStay() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_STAY);
        protected virtual void OnTriggerExit() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_EXIT);
        
        protected virtual void OnCollisionEnter() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_ENTER);
        protected virtual void OnCollisionStay() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_STAY);
        protected virtual void OnCollisionExit() => RaiseEventWithStrategy(TriggerStrategy.RAISE_ON_EXIT);
        
        protected virtual void RaiseEventWithStrategy(TriggerStrategy strategy) {
            if (_strategy == strategy) _gameEvent.Raise();
        }
    }
}
