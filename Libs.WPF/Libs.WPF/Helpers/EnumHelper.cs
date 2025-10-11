using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Libs.WPF.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<KeyValuePair<Enum, string>> GetAllValuesAndDescriptions<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<Enum>()
                       .Select(e => new KeyValuePair<Enum, string>(e, GetDescription(e)));
        }

        public static string GetDescription(Enum value)
        {
            return value.GetType()
                        .GetField(value.ToString())
                        ?.GetCustomAttribute<DescriptionAttribute>()?.Description
                   ?? value.ToString();
        }
    }
}
