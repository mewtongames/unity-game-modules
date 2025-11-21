using System;
using System.Collections.Generic;

namespace MewtonGames.Common
{
    public class StatefulObject<T> where T : Enum
    {
        public event Action<T> stateChanged;

        public T currentState { get; private set; }
        public T previousState { get; private set; }

        private readonly Dictionary<T, Action> _transitions = new Dictionary<T, Action>();

        protected void AddTransition(T state, Action stateChangeHandler)
        {
            _transitions.TryAdd(state, stateChangeHandler);
        }

        protected void SetState(T state)
        {
            previousState = currentState;
            currentState = state;

            if (_transitions.TryGetValue(state, out var transition))
            {
                transition();
            }

            stateChanged?.Invoke(currentState);
        }
    }
}