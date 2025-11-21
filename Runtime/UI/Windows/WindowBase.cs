using MewtonGames.Audio;
using MewtonGames.Common;
using UnityEngine;

namespace MewtonGames.UI.Windows
{
    [RequireComponent(typeof(Canvas))]
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        public bool isFocused { get; protected set; }

        [SerializeField] protected Canvas _canvas;

        protected IAudioModule _audioModule;
        protected IUIModule _uiModule;

        public void SetSortingOrder(int sortingOrder)
        {
            _canvas.sortingOrder = sortingOrder;
        }

        public virtual void OnFocused()
        {
            isFocused = true;
        }

        public virtual void OnLostFocus()
        {
            isFocused = false;
        }

        protected virtual void Awake()
        {
            _uiModule = GameContext.Get<IUIModule>();
            _audioModule = GameContext.Get<IAudioModule>();
        }
    }
}