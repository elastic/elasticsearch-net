// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

internal static class SerializerExtensions
{
	public static bool TryGetJsonSerializerOptions(this Serializer serializer, out JsonSerializerOptions? options)
	{
		if (serializer is DefaultRequestResponseSerializer defaultSerializer)
		{
			options = defaultSerializer.Options;
			return true;
		}

		options = null;
		return false;
	}
}
