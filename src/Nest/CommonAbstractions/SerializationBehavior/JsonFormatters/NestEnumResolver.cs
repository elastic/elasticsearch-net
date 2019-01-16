using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Utf8Json;
using Utf8Json.Formatters;

namespace Nest
{
	/// <summary>
	/// EnumResolver that aligns with how enums are serialized with the client
	/// when using Json.NET.
	/// Defaults to numeric value, unless attributed with StringEnumAttribute
	/// or any field has EnumMemberAttribute or DataMemberAttribute applied.
	/// </summary>
	internal sealed class NestEnumResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new NestEnumResolver();

		private NestEnumResolver() { }

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				var ti = typeof(T).GetTypeInfo();

				if (ti.IsNullable())
				{
					// build underlying type and use wrapped formatter.
					ti = ti.GenericTypeArguments[0].GetTypeInfo();
					if (!ti.IsEnum) return;

					var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
					if (innerFormatter == null) return;

					formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()),
						new object[] { innerFormatter });
				}
				else if (typeof(T).IsEnum)
				{
					var stringEnumAttribute = typeof(T).GetCustomAttribute<StringEnumAttribute>();

					if (stringEnumAttribute != null)
						formatter = new EnumFormatter<T>(true);
					else
					{
						formatter = new EnumFormatter<T>(false);
					}
				}
			}
		}
	}

	// TODO: Move to Elasticsearch.Net and register with serializer in low level client
	internal sealed class ElasticsearchNetEnumResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new ElasticsearchNetEnumResolver();

		private ElasticsearchNetEnumResolver() { }

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				var ti = typeof(T).GetTypeInfo();

				if (ti.IsNullable())
				{
					// build underlying type and use wrapped formatter.
					ti = ti.GenericTypeArguments[0].GetTypeInfo();
					if (!ti.IsEnum) return;

					var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
					if (innerFormatter == null) return;

					formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()),
						new object[] { innerFormatter });
				}
				else if (typeof(T).IsEnum)
				{
					var stringEnumAttribute = typeof(T).GetCustomAttribute<Elasticsearch.Net.StringEnumAttribute>();

					if (stringEnumAttribute != null)
						formatter = new EnumFormatter<T>(true);
				}
			}
		}
	}
}
