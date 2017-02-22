using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DomainBasedFolderOrganizer
{
    public static class Util
    {
        public static string GetDescription<T>(this Enum value) where T : DescriptionAttribute
        {
            if (value != null)
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo != null)
                {
                    var attribute = fieldInfo.GetCustomAttributes(typeof(T), false).SingleOrDefault() as T;
                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }
            }

            return null;
        }

        public static T GetEnumValue<T, U>(this string description) where U : DescriptionAttribute
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("description");
            }

            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(typeof(U), false), (f, a) => new { Field = f, Att = a })
                            .Where(a => (a.Att as U).Description == description).SingleOrDefault();

            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
    }
}
