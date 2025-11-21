using MewtonGames.Helpers;
using UnityEngine.EventSystems;

namespace MewtonGames.Extensions
{
    public static class PhysicsExtensions
    {
        public static void SetLayerEventEnabled(this PhysicsRaycaster raycaster, string layer, bool value)
        {
            if (value)
            {
                raycaster.eventMask |= LayerHelper.ConvertNameToMask(layer);
            }
            else
            {
                raycaster.eventMask &= ~LayerHelper.ConvertNameToMask(layer);
            }
        }

        public static bool IsLayerEnabled(this PhysicsRaycaster raycaster, string layer)
        {
            return (raycaster.eventMask & LayerHelper.ConvertNameToMask(layer)) > 0;
        }
    }
}