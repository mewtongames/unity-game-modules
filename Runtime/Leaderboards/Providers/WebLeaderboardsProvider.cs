#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using Playgama;

namespace MewtonGames.Leaderboards.Providers
{
    public class WebLeaderboardsProvider : ILeaderboardsProvider
    {
        public LeaderboardType type { get; }

        public WebLeaderboardsProvider()
        {
            switch (Bridge.leaderboards.type)
            {
                case Playgama.Modules.Leaderboards.LeaderboardType.NotAvailable:
                    type = LeaderboardType.NotAvailable;
                    break;
                case Playgama.Modules.Leaderboards.LeaderboardType.InGame:
                    type = LeaderboardType.InGame;
                    break;
                case Playgama.Modules.Leaderboards.LeaderboardType.Native:
                    type = LeaderboardType.Native;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void SetScore(string id, int score)
        {
            Bridge.leaderboards.SetScore(id, score);
        }

        public void GetEntries(string id, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.leaderboards.GetEntries(id, onComplete);
        }
    }
}
#endif