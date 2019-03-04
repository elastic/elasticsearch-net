using System;
using System.Collections.Concurrent;

namespace Elasticsearch.Net {
	internal class ElasticsearchNetFormatterResolver : IJsonFormatterResolver
	{
		private readonly IJsonFormatter<object> _fallbackFormatter;
		private readonly InnerResolver _innerFormatterResolver;

		public static ElasticsearchNetFormatterResolver Instance => new ElasticsearchNetFormatterResolver();
		
		public ElasticsearchNetFormatterResolver()
		{
			_innerFormatterResolver = new InnerResolver();
			_fallbackFormatter = new DynamicObjectTypeFallbackFormatter(_innerFormatterResolver);
		}

		public IJsonFormatter<T> GetFormatter<T>() =>
			typeof(T) == typeof(object)
				? (IJsonFormatter<T>)_fallbackFormatter
				: _innerFormatterResolver.GetFormatter<T>();

		internal sealed class InnerResolver : IJsonFormatterResolver
		{
			private static readonly IJsonFormatterResolver[] Resolvers =
			{
				BuiltinResolver.Instance, // Builtin primitives
				ElasticsearchNetEnumResolver.Instance, // Specialized Enum handling
				AttributeFormatterResolver.Instance, // [JsonFormatter]
				DynamicGenericResolver.Instance, // T[], List<T>, etc...
			};

			private readonly IJsonFormatterResolver _finalFormatter;
			private readonly ConcurrentDictionary<Type, object> _formatters = new ConcurrentDictionary<Type, object>();
	
			internal InnerResolver()		
			{
				_finalFormatter =
					DynamicObjectResolver.Create(null, new Lazy<Func<string, string>>(() => StringMutator.Original), true);
			}

			public IJsonFormatter<T> GetFormatter<T>() =>
				(IJsonFormatter<T>)_formatters.GetOrAdd(typeof(T), type =>
				{
					foreach (var item in Resolvers)
					{
						var formatter = item.GetFormatter<T>();
						if (formatter != null)
							return formatter;
					}

					return _finalFormatter.GetFormatter<T>();
				});
		}
	}
}