// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Mapping;
#else
using Elastic.Clients.Elasticsearch.Mapping;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;
#else
namespace Elastic.Clients.Elasticsearch.IndexManagement;
#endif

public partial class GetMappingResponse
{
	[JsonIgnore]
	public IReadOnlyDictionary<IndexName, IndexMappingRecord> Indices => BackingDictionary;
}

public static class GetMappingResponseExtensions
{
	public static TypeMapping GetMappingFor<T>(this GetMappingResponse response) => response.GetMappingFor(typeof(T));

	public static TypeMapping GetMappingFor(this GetMappingResponse response, IndexName index)
	{
		if (index.IsNullOrEmpty())
			return null;

		return response.Indices.TryGetValue(index, out var indexMappings) ? indexMappings.Mappings : null;
	}
}
