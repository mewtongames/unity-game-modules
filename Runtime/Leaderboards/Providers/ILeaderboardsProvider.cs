using System;
using System.Collections.Generic;

namespace MewtonGames.Leaderboards.Providers
{
    public interface ILeaderboardsProvider
    {
        public LeaderboardType type { get; }

        public void SetScore(string id, int score);

        public void GetEntries(string id, Action<bool, List<Dictionary<string, string>>> onComplete);
    }
}