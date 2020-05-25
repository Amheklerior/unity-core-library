using System;
using System.Diagnostics;

namespace Amheklerior.Core.EventSystem {
    

    public class Event : IEvent {

        private Action OnEventRaised;

        public virtual void Raise() => OnEventRaised?.Invoke();
        public virtual void Subscribe(Action eventReaction) => OnEventRaised += eventReaction;
        public virtual void Unsubscribe(Action eventReaction) => OnEventRaised -= eventReaction;
        public virtual void UnsibscribeAll() => OnEventRaised = null;
    }


    public class Event<TEventData> : IEvent<TEventData> {

        private Action<TEventData> OnEventRaised;

        public virtual void Raise(TEventData data) => OnEventRaised?.Invoke(data);
        public virtual void Subscribe(Action<TEventData> eventReaction) => OnEventRaised += eventReaction;
        public virtual void Unsubscribe(Action<TEventData> eventReaction) => OnEventRaised -= eventReaction;
        public virtual void UnsibscribeAll() => OnEventRaised = null;
    }

    
    #region Debuggable versions 

    internal sealed class DebuggableEvent : Event {

        public override void Raise() {
            Debug.Write($"The event {this} has been raised!");
            base.Raise();
        }

        public override void Subscribe(Action eventReaction) {
            base.Subscribe(eventReaction);
            Debug.Write($"An action subscribed as a reaction to the event: {this}.");
        }

        public override void Unsubscribe(Action eventReaction) {
            base.Unsubscribe(eventReaction);
            Debug.Write($"An action unsubscribed from the event: {this}.");
        }

        public override void UnsibscribeAll() {
            base.UnsibscribeAll();
            Debug.Write($"All actions unsubscribed from the event: {this}.");
        }
    }

    internal sealed class DebuggableEvent<TEventData> : Event<TEventData> {

        public override void Raise(TEventData data) {
            Debug.Write($"The event {this} has been raised with the data: {data}");
            base.Raise(data);
        }

        public override void Subscribe(Action<TEventData> eventReaction) {
            base.Subscribe(eventReaction);
            Debug.Write($"An action subscribed as a reaction to the event: {this}.");
        }

        public override void Unsubscribe(Action<TEventData> eventReaction) {
            base.Unsubscribe(eventReaction);
            Debug.Write($"An action unsubscribed from the event: {this}.");
        }

        public override void UnsibscribeAll() {
            base.UnsibscribeAll();
            Debug.Write($"All actions unsubscribed from the event: {this}.");
        }
    }

    #endregion

}
