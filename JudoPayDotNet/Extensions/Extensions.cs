using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Extensions
{
    public static class JudoPayExtensions
    {
        public static FieldInfo GetRuntimeField(this Type type, string name)
        {
            return type.GetField(name);
        }
        public static FieldInfo[] GetRuntimeFields(this Type type)
        {
            return type.GetFields();
        }

        public static object[] GetCustomAttributes(this FieldInfo field, Type attributeType)
        {
            return field.GetCustomAttributes(attributeType, false);
        }
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

    }
}
