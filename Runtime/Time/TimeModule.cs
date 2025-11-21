using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MewtonGames.Time
{
    public class TimeModule : MonoBehaviour, ITimeModule
    {
        public bool isInitialized { get; private set; }

        private readonly List<ITimer> _timers = new();

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public ITimer Schedule(float intervalInSeconds, bool isUnscaled = false, bool withStarting = true)
        {
            var newTimer = new Timer(intervalInSeconds, isUnscaled);
            _timers.Add(newTimer);

            if (withStarting)
            {
                newTimer.Start();
            }

            return newTimer;
        }

        public ITimer Schedule(TimeSpan interval, bool isUnscaled = false, bool withStarting = true)
        {
            var newTimer = new Timer(interval, isUnscaled);
            _timers.Add(newTimer);

            if (withStarting)
            {
                newTimer.Start();
            }

            return newTimer;
        }

        private void Update()
        {
            var isPause = UnityEngine.Time.timeScale <= 0f;
            var timersToRemove = new List<ITimer>();

            foreach (var timer in _timers.ToList())
            {
                if (timer.currentState == TimerState.Completed || timer.currentState == TimerState.Canceled)
                {
                    timersToRemove.Add(timer);
                    continue;
                }

                if (timer.currentState != TimerState.Started)
                {
                    continue;
                }

                if (!timer.isUnscaled && isPause)
                {
                    continue;
                }

                timer.Update(UnityEngine.Time.deltaTime);
            }

            foreach (var timer in timersToRemove)
            {
                _timers.Remove(timer);
            }
        }
    }
}