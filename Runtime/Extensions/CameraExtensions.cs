using MewtonGames.Helpers;
using UnityEngine;

namespace MewtonGames.Extensions
{
    public static class CameraExtensions
    {
        public static void SetLayerCullingEnabled(this Camera camera, string layer, bool value)
        {
            if (value)
            {
                camera.cullingMask |= LayerHelper.ConvertNameToMask(layer);
            }
            else
            {
                camera.cullingMask &= ~LayerHelper.ConvertNameToMask(layer);
            }
        }

        public static bool IsLayerCullingEnabled(this Camera camera, string layer)
        {
            return (camera.cullingMask & LayerHelper.ConvertNameToMask(layer)) > 0;
        }
    }
}