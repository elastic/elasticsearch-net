using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class ElasticsearchFormatterResolver : IJsonFormatterResolver
	{
		private static readonly IJsonFormatter<object> FallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);
		public IConnectionSettingsValues Settings { get; }

		public ElasticsearchFormatterResolver(IConnectionSettingsValues settings) => Settings = settings;

		public IJsonFormatter<T> GetFormatter<T>()
		{
			var formatter = FormatterCache<T>.Formatter;

			return formatter;
		}

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
					Formatter = (IJsonFormatter<T>)FallbackFormatter;
				else
					Formatter = InnerResolver.Instance.GetFormatter<T>();
			}
		}

		internal sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			private static readonly IJsonFormatterResolver[] Resolvers =
			{
				// IL emit a resolver that registers formatters
				DynamicCompositeResolver.Create(new IJsonFormatter[]
				{
					new QueryContainerCollectionFormatter(),
					new SimpleQueryStringFlagsFormatter(),
				}, new IJsonFormatterResolver[0]),
				BuiltinResolver.Instance, // Builtin
				EnumResolver.Default, // Enum(default => string)
				DynamicGenericResolver.Instance, // T[], List<T>, etc...
				AttributeFormatterResolver.Instance, // [JsonFormatter]
				ReadAsFormatterResolver.Instance,
				NestGenericSourceTypeFormatterResolver.Instance,
				DynamicObjectResolver.ExcludeNullCamelCase

			};

			private InnerResolver() { }

			public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

			private static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> Formatter;

				static FormatterCache()
				{
					foreach (var item in Resolvers)
					{
						var f = item.GetFormatter<T>();
						if (f != null)
						{
							Formatter = f;
							return;
						}
					}
				}
			}
		}
	}
}
