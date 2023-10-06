// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

internal static class JsonSerializerOptionsExtensions
{
	internal static bool TryGetClientSettings(this JsonSerializerOptions options, out IElasticsearchClientSettings settings) =>
		ElasticsearchClient.SettingsTable.TryGetValue(options, out settings);
}
