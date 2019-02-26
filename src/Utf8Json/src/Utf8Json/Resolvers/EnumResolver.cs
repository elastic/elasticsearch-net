using System;
using Utf8Json.Internal.Emit;
using Utf8Json.Internal;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Formatters;
using Utf8Json.Resolvers.Internal;

namespace Utf8Json.Resolvers
{
    public static class EnumResolver
    {
        /// <summary>Serialize as Name.</summary>
        public static readonly IJsonFormatterResolver Default = EnumDefaultResolver.Instance;
        /// <summary>Serialize as Value.</summary>
        public static readonly IJsonFormatterResolver UnderlyingValue = EnumUnderlyingValueResolver.Instance;
    }
}

namespace Utf8Json.Resolvers.Internal
{
    internal sealed class EnumDefaultResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumDefaultResolver();

        EnumDefaultResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (ti.IsNullable())
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumFormatter<T>(true);
                }
            }
        }
    }

    internal sealed class EnumUnderlyingValueResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumUnderlyingValueResolver();

        EnumUnderlyingValueResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (ti.IsNullable())
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumFormatter<T>(false);
                }
            }
        }
    }
}