using UnityEngine;
using System;
using Amheklerior.Core.Common;

namespace Amheklerior.Core.EventSystem {
    
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "Plain event", order = 1)]
    public class GameEvent : RichScriptableObject, IEvent {
        
        protected IEvent _event;

        private void OnEnable() {
#if UNITY_EDITOR
            _event = _debugMode ? new DebuggableEvent() : new Event();
            _description = _description ?? $"{name} event";
#else
            _event = new Event();
#endif
        }
        
        private void OnDisable() => UnsibscribeAll();

        public void Raise() => _event.Raise();
        public void Subscribe(Action eventReaction) => _event.Subscribe(eventReaction);
        public void Unsubscribe(Action eventReaction) => _event.Unsubscribe(eventReaction);
        public void UnsibscribeAll() => _event.UnsibscribeAll();
    }
    
    
    public abstract class GameEvent<TEventData> : RichScriptableObject, IEvent<TEventData> {
        
        private IEvent<TEventData> _event;

        private void OnEnable() {
#if UNITY_EDITOR
            _event = _debugMode ? new DebuggableEvent<TEventData>() : new Event<TEventData>();
            _description = _description ?? $"{name} event";
#else
            _event = new Event<TEventData>();
#endif
        }

        private void OnDisable() => UnsibscribeAll();

        public void Raise(TEventData data) => _event.Raise(data);
        public void Subscribe(Action<TEventData> eventReaction) => _event.Subscribe(eventReaction);
        public void Unsubscribe(Action<TEventData> eventReaction) => _event.Unsubscribe(eventReaction);
        public void UnsibscribeAll() => _event.UnsibscribeAll();
    }

}
