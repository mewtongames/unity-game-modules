using UnityEngine;

namespace MewtonGames.Helpers
{
    public static class LayerHelper
    {
        public static int ConvertNameToMask(string layer)
        {
            return ConvertIndexToMask(LayerMask.NameToLayer(layer));
        }

        public static int ConvertIndexToMask(int layerIndex)
        {
            return 1 << layerIndex;
        }
    }
}