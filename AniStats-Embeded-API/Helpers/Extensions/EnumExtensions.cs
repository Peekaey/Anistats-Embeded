using System.ComponentModel;
using System.Reflection;

namespace AniStats_Embeded_API.Helpers.Extensions;

public static class EnumExtensions
{
    public static string? GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            
            if (attribute != null)
            {
                return attribute.Description;
            }
        }
        return null;
    }
}