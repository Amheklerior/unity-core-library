using UnityEngine;
using System;
using Amheklerior.Core.Common;

namespace Amheklerior.Core.EventSystem {
    
    [CreateAssetMenu(menuName = "Core/Event System/Game Event", order = 1)]
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


    #region Some basic typed-game-event definitions 

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (int)", order = 2)]
    public class GameEventInt : GameEvent<int> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (float)", order = 3)]
    public class GameEventFloat : GameEvent<float> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (boolean)", order = 4)]
    public class GameEventBool : GameEvent<bool> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (string)", order = 5)]
    public class GameEventString : GameEvent<string> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (2D vector)", order = 6)]
    public class GameEventVector2 : GameEvent<Vector2> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (2D int vector)", order = 7)]
    public class GameEventVector2Int : GameEvent<Vector2Int> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (3D vector)", order = 8)]
    public class GameEventVector3 : GameEvent<Vector3> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (3D int vector)", order = 9)]
    public class GameEventVector3Int : GameEvent<Vector3Int> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (game object)", order = 10)]
    public class GameEventGameObject : GameEvent<GameObject> { }

    [CreateAssetMenu(menuName = "Core/Event System/Game Event (scriptable object)", order = 11)]
    public class GameEventScriptableObject : GameEvent<ScriptableObject> { }

    #endregion

}
