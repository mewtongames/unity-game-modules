using UnityEngine;

namespace MewtonGames.Extensions
{
    public static class GameObjectExtensions
    {
        public static void ChangeLayerRecursively(this GameObject gameObject, int layerIndex)
        {
            gameObject.layer = layerIndex;

            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.ChangeLayerRecursively(layerIndex);
            }
        }

        public static void ChangeLayerRecursively(this GameObject gameObject, string layer)
        {
            var layerIndex = LayerMask.NameToLayer(layer);
            gameObject.ChangeLayerRecursively(layerIndex);
        }
    }
}