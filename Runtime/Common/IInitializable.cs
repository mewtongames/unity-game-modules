using System;

namespace MewtonGames.Common
{
    public interface IInitializable
    {
        public bool isInitialized { get; }
        public void Initialize(Action onComplete = null);
    }
    
    public interface IInitializable<T>
    {
        public bool isInitialized { get; }
        public void Initialize(T settings, Action onComplete = null);
    }
}