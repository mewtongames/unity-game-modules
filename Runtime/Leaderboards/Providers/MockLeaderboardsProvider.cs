using System;
using System.Collections.Generic;

namespace MewtonGames.Leaderboards.Providers
{
    public class MockLeaderboardsProvider : ILeaderboardsProvider
    {
        public LeaderboardType type => LeaderboardType.NotAvailable;

        public void SetScore(string id, int score) { }

        public void GetEntries(string id, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            onComplete?.Invoke(false, new List<Dictionary<string, string>>());
        }
    }
}