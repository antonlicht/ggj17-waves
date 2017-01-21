using System;

namespace Shared.Extensions
{
    public static class EnumHelper
    {
        public static T Parse<T>(string value, T defaultValue = default(T), bool ignoreCase = false)
        {
            if (!Enum.IsDefined(typeof(T), value))
                return defaultValue;

            return (T) Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}