using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	internal static class ConnectionSettingsValuesExtensions
	{
		public static InternalSerializer CreateStateful<T>(this IConnectionSettingsValues settings, IJsonFormatter<T> formatter)
		{
			var currentFormatterResolver = ((InternalSerializer)settings.RequestResponseSerializer).FormatterResolver;
			var formatterResolver = new StatefulFormatterResolver<T>(formatter, currentFormatterResolver);
			return StatefulSerializerFactory.CreateStateful(settings, formatterResolver);
		}
	}

	internal class StatefulFormatterResolver<TStateful> : IJsonFormatterResolver, IJsonFormatterResolverWithSettings
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
