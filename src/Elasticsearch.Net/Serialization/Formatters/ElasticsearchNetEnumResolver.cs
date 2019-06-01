using System;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Serialization.Formatters
{
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
					var stringEnumAttribute = typeof(T).GetCustomAttribute<StringEnumAttribute>();
					formatter = stringEnumAttribute != null
						? new EnumFormatter<T>(true)
						: new EnumFormatter<T>(false);
				}
			}
		}
	}
}
