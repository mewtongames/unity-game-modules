using System;
using MewtonGames.Common;

namespace MewtonGames.Time
{
    public interface ITimeModule : IInitializable
    {
        public ITimer Schedule(float intervalInSeconds, bool isUnscaled = false, bool withStarting = true);
        public ITimer Schedule(TimeSpan interval, bool isUnscaled = false, bool withStarting = true);
    }
}