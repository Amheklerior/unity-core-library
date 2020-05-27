
namespace Amheklerior.Core.Time {

    public interface ITimer {
        bool IsRunning { get; }
        void Start();
        void Pause();
        void Resume();
        void Stop();
        void Restart();
    }

}