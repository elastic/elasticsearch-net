// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	internal static class StatefulSerializerExtensions
	{
		public static DefaultHighLevelSerializer CreateStateful<T>(this ITransportSerializer serializer, IJsonFormatter<T> formatter)
		{
			if (!(serializer is IInternalSerializer s) || !s.TryGetJsonFormatter(out var currentFormatterResolver))
				throw new Exception($"Can not create a stateful serializer because {serializer.GetType()} does not yield a json formatter");

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
