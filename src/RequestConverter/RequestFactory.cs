using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Transport;

namespace RequestConverter;

internal static partial class RequestFactory
{
	public static Request? Materialize(
		Serializer serializer,
		string id,
		IReadOnlyDictionary<string, string>? queryParameters,
		IReadOnlyDictionary<string, string>? pathParameters,
		string? body)
	{
		ArgumentNullException.ThrowIfNull(serializer);
		ArgumentException.ThrowIfNullOrEmpty(id);

		if (!Lookup.TryGetValue(id, out var factory))
		{
			return null;
		}

		return factory(serializer, pathParameters, queryParameters, body);
	}
}
