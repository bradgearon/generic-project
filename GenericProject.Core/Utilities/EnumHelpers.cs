using System;

namespace GenericProject.Core.Utilities
{
    public class EnumHelpers
    {
        public static T? GetFromFilter<T>(string value) where T: struct
        {
            if (value == null)
                return null;

            T enumValue;
            value = value.Replace(" ", string.Empty);
            if (!Enum.TryParse(value, true, out enumValue))
                return default(T?);
            return enumValue;
        }
    }
}