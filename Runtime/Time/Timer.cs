using System;

namespace Amheklerior.Core.Time {
    
    public class Timer : ITimer {

        public float Current { get; private set; }

        public bool IsRunning { get; private set; }

        public Timer(float? expiryTimeInSeconds = null, Action OnTimeExpired = null) {
            if (expiryTimeInSeconds != null && expiryTimeInSeconds <= 0)
                throw new ArgumentException($"The expiry time must be greater than zero. Was {expiryTimeInSeconds} instead.");

            _expiryTime = expiryTimeInSeconds;
            _onTimeExpired = OnTimeExpired;
        }

        public void Start() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public void Resume() => IsRunning = true;

        public void Stop() {
            Current = 0;
            IsRunning = false;
        }

        public void Restart() {
            Current = 0;
            IsRunning = true;
        }

        public void Tick(float elapsed) {
            if (!IsRunning || Expired) return;
            Current += elapsed;
            if (Expired) _onTimeExpired?.Invoke();
        }

        #region Internals 

        private readonly Action _onTimeExpired;
        private readonly float? _expiryTime;
        private bool Expired => _expiryTime != null && Current >= _expiryTime;

        #endregion

    }
}
