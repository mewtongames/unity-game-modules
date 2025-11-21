using System;
using System.Collections.Generic;

namespace MewtonGames.Common
{
    public class ActionsChain
    {
        private readonly Queue<Action<Action>> _list = new();
        private readonly Action _onComplete;
        private bool _isStarted;


        public ActionsChain(Action onComplete)
        {
            _onComplete = onComplete;
        }

        public ActionsChain(Action<Action> action, Action onComplete)
        {
            Add(action);
            _onComplete = onComplete;
        }

        
        public ActionsChain Add(Action<Action> action)
        {
            if (!_isStarted)
            {
                _list.Enqueue(action);
            }

            return this;
        }

        public void Run()
        {
            if (_isStarted)
            {
                return;
            }

            _isStarted = true;
            Next();
        }


        private void Next()
        {
            if (_list.Count <= 0)
            {
                _onComplete?.Invoke();
                return;
            }

            var action = _list.Dequeue();
            action?.Invoke(Next);
        }
    }
}