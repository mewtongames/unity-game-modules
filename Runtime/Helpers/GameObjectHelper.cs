using UnityEngine;

namespace MewtonGames.Helpers
{
    public static class GameObjectHelper
    {
        public static T CreateDontDestroy<T>(string name = null) where T : Component
        {
            var component = Create<T>(name);
            Object.DontDestroyOnLoad(component.gameObject);
            return component;
        }

        public static T Create<T>(string name = null) where T : Component
        {
            var gameObject = new GameObject(name ?? typeof(T).Name);
            var component = gameObject.AddComponent<T>();
            return component;
        }
    }
}