using System;
using MewtonGames.UI.Windows;

namespace MewtonGames.UI.Providers
{
    public interface IWindowsProvider
    {
        public void Instantiate<T>(Action<T> onComplete) where T : WindowBase;
        public void Destroy(IWindow window);
    }
}