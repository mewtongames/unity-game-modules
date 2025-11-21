using UnityEngine;

namespace MewtonGames.UI.Windows
{
    public interface IWindow
    {
        public Transform transform { get; }
        public void SetSortingOrder(int sortingOrder);
        public void OnFocused();
        public void OnLostFocus();
    }
}