using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal static class StatefulSerializerExtensions
	{
		public static DefaultHighLevelSerializer CreateStateful<T>(this IElasticsearchSerializer serializer, IJsonFormatter<T> formatter)
		{
			var currentFormatterResolver = ((IInternalSerializerWithFormatter)serializer).FormatterResolver;
			var formatterResolver = new StatefulFormatterResolver<T>(formatter, currentFormatterResolver);
			return new DefaultHighLevelSerializer(formatterResolver);
		}

		private class StatefulFormatterResolver<TStateful> : IJsonFormatterResolver, IJsonFormatterResolverWithSettings
		{
			private readonly IJsonFormatter<TStateful> _jsonFormatter;
			private readonly IJsonFormatterResolver _formatterResolver;

			public StatefulFormatterResolver(IJsonFormatter<TStateful> jsonFormatter, IJsonFormatterResolver formatterResolver)
			{
				_jsonFormatter = jsonFormatter;
				_formatterResolver = formatterResolver;
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				if (typeof(T) == typeof(TStateful))
					return (IJsonFormatter<T>)_jsonFormatter;

				return _formatterResolver.GetFormatter<T>();
			}

			public IConnectionSettingsValues Settings => ((IJsonFormatterResolverWithSettings)_formatterResolver).Settings;
		}
	}
}
