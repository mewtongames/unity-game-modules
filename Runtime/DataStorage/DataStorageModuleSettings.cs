using MewtonGames.DataStorage.Providers;
using MewtonGames.Time;

namespace MewtonGames.DataStorage
{
    public class DataStorageModuleSettings
    {
        public readonly IDataStorageProvider provider;
        public readonly ITimeModule timeModule;
        public readonly bool enableSetRequestsQueue;
        public readonly int minSetRequestsInterval;

        public DataStorageModuleSettings(IDataStorageProvider provider)
        {
            this.provider = provider;
        }

        public DataStorageModuleSettings(IDataStorageProvider provider, ITimeModule timeModule, bool enableSetRequestsQueue, int minSetRequestsInterval)
        {
            this.provider = provider;
            this.timeModule = timeModule;
            this.enableSetRequestsQueue = enableSetRequestsQueue;
            this.minSetRequestsInterval = minSetRequestsInterval;
        }
    }
}