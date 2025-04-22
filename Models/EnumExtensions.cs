using System;
using System.ComponentModel.DataAnnotations;

namespace CDIO.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute == null ? value.ToString() : attribute.Name;
        }
    }
}
