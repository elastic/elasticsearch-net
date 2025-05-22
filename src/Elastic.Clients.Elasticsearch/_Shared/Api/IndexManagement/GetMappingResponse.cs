// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Mapping;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public static class GetMappingResponseExtensions
{
	public static TypeMapping? GetMappingFor(this GetMappingResponse response, string index)
	{
		if (index.IsNullOrEmpty())
			return null;

		return response.Values.TryGetValue(index, out var indexMappings) ? indexMappings.Mappings : null;
	}
}
