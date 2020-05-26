using System;

namespace Amheklerior.Core.EventSystem {

    public interface IEvent {
        void Raise();
        void Subscribe(Action eventReaction);
        void Unsubscribe(Action eventReaction);
        void UnsibscribeAll();
    }

    public interface IEvent<TEventData> {
        void Raise(TEventData data);
        void Subscribe(Action<TEventData> eventReaction);
        void Unsubscribe(Action<TEventData> eventReaction);
        void UnsibscribeAll();
    }

}
