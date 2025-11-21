using MewtonGames.Common;
using MewtonGames.DataStorage.Providers;

namespace MewtonGames.DataStorage
{
    public interface IDataStorageModule : IDataStorageProvider, IInitializable
    {
        public void ForceSetQueuedData();
    }
}