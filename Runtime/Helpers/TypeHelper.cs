using System;

namespace MewtonGames.Helpers
{
    public static class TypeHelper
    {
        public static bool IsPrimitiveOrString(Type type)
        {
            return type.IsPrimitive || type == typeof(string);
        }
    }
}