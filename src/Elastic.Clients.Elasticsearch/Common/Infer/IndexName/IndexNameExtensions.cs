// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;
//public static class IndexNameExtensions
//{
//	public static string? Resolve(this IndexName? marker, IElasticsearchClientSettings elasticsearchClientSettings)
//	{
//		if (marker == null)
//			return null;

//		elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));

//		return marker.Type == null
//			? marker.Name
//			: new IndexNameResolver(elasticsearchClientSettings).Resolve(marker.Type);
//	}
//}
