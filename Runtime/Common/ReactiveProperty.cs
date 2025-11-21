using System;

namespace MewtonGames.Common
{
    public class ReactiveProperty<T>
    {
        public event Action<T> changed;

        public T value { get; protected set; }
        public bool hasValue { get; private set; }

        public ReactiveProperty() { }

        public ReactiveProperty(T value)
        {
            SetValue(value);
        }

        public void SetValue(T value)
        {
            this.value = value;
            hasValue = true;
            changed?.Invoke(value);
        }
    }
}