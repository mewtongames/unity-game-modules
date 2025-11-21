using System;

namespace MewtonGames.Time
{
    public interface ITimer
    {
        public event Action<TimerState> stateChanged;
        public event Action<TimeSpan> changed;
        public event Action oneSecondPassed;
        public event Action<float> progressChanged;

        public TimerState currentState { get; }
        public TimerState previousState { get; }

        public TimeSpan timeTotal { get; }
        public TimeSpan timeLeft { get; }

        public bool isUnscaled { get; }
        public float progress { get; }

        public void Start();
        public void Restart();
        public void Pause(bool value);
        public void Update(float deltaTime);
        public void Cancel();

        public void Set(TimeSpan timeSpan);
        public void Set(float seconds);

        public void Add(TimeSpan timeSpan);
        public void Add(float seconds);

        public void Subtract(TimeSpan timeSpan);
        public void Subtract(float seconds);
    }
}