// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
		string? body,
		ICollection<string> unsupportedParameters)
	{
		ArgumentNullException.ThrowIfNull(serializer);
		ArgumentException.ThrowIfNullOrEmpty(id);
		ArgumentNullException.ThrowIfNull(unsupportedParameters);

		if (!Lookup.TryGetValue(id, out var factory))
		{
			return null;
		}

		return factory(serializer, pathParameters, queryParameters, body, unsupportedParameters);
	}
}
