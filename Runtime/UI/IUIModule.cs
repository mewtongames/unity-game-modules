using System;
using System.Collections.Generic;
using MewtonGames.Common;
using MewtonGames.UI.Providers;
using MewtonGames.UI.Windows;

namespace MewtonGames.UI
{
    public interface IUIModule : IInitializable<IWindowsProvider>
    {
        public void Open<T>(int stageIndex = 0, Action<T> onComplete = null) where T : WindowBase;

        public List<T> Get<T>(int stageIndex = 0) where T : WindowBase;
        public List<T> GetFromAllStages<T>() where T : WindowBase;

        public void Close(IWindow window);
        public void Close<T>(int stageIndex = 0) where T : WindowBase;
        public void CloseAll(int stageIndex = 0);
        public void CloseInAllStages<T>() where T : WindowBase;
        public void CloseAllInAllStages();
    }
}