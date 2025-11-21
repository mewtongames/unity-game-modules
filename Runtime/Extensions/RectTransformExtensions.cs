using UnityEngine;

namespace MewtonGames.Extensions
{
    public static class RectTransformExtensions
    {
        public static Vector3 GetScreenPosition(this RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners[0] + (corners[2] - corners[0]) / 2f;
        }
    }
}