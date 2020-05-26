
namespace Amheklerior.Core.EventSystem {

    public interface IEventListener {
        void OnEventRaised();
    }
    
    public interface IEventListener<TEventData> {
        void OnEventRaised(TEventData data);
    }

}
