using System;
using System.Collections.Generic;
using MewtonGames.Leaderboards.Providers;

namespace MewtonGames.Leaderboards
{
    public class LeaderboardsModule : ILeaderboardsProvider
    {
        public LeaderboardType type => _provider.type;

        private readonly ILeaderboardsProvider _provider;

        public LeaderboardsModule(ILeaderboardsProvider provider)
        {
            _provider = provider;
        }

        public void SetScore(string id, int score)
        {
            _provider.SetScore(id, score);
        }

        public void GetEntries(string id, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            _provider.GetEntries(id, onComplete);
        }
    }
}