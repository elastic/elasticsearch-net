// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Playground;

RequestConfiguration? globalRequestConfiguration = null;
ConditionalWeakTable<RequestConfiguration, RequestConfiguration>? globalRequestConfigurations = null;

var settings = new ElasticsearchClientSettings(new Uri("https://primary.es.europe-west3.gcp.cloud.es.io"))
	.Authentication(new BasicAuthentication("elastic", "Oov35Wtxj5DzpZNzYAzFb0KZ"))
	.OnBeforeRequest(OnBeforeRequest)
	.EnableDebugMode(cd => Console.WriteLine(cd.DebugInformation));
void OnBeforeRequest(ElasticsearchClient client, Request request, EndpointPath endpointPath, ref PostData? postData, ref IRequestConfiguration? requestConfiguration)
{
	// Each time a request is made, the transport creates a new `BoundConfiguration` for every `IRequestConfiguration`
	// that is not in the cache (based on reference equality).

	// To prevent frequent allocations of our mutated request configurations (and the secondary allocations for
	// `BoundConfiguration`), we have to maintain a custom cache that maps every original request configuration to the
	// mutated one.
	
	if (requestConfiguration is null)
	{
		globalRequestConfiguration = Interlocked.CompareExchange(
			ref globalRequestConfiguration,
			new RequestConfiguration
			{
				UserAgent = UserAgent.Create("my-custom-user-agent")
			},
			null) ?? globalRequestConfiguration;

		requestConfiguration = globalRequestConfiguration;
		return;
	}

	if (requestConfiguration is not RequestConfiguration rc)
	{
		// Only `RequestConfiguration` (not all implementations of `IRequestConfiguration`) gets cached in the
		// internal cache.
		requestConfiguration = MutateRequestConfiguration(requestConfiguration);
		return;
	}

	// ReSharper disable InconsistentlySynchronizedField

	var cache = (Interlocked.CompareExchange(
		ref globalRequestConfigurations,
		new ConditionalWeakTable<RequestConfiguration, RequestConfiguration>(),
		null
	) ?? globalRequestConfigurations);

	if (cache.TryGetValue(rc, out var mutatedRequestConfiguration))
	{
		requestConfiguration = mutatedRequestConfiguration;
		return;
	}

	mutatedRequestConfiguration = MutateRequestConfiguration(rc);

#if NET8_0_OR_GREATER
	cache.TryAdd(rc, mutatedRequestConfiguration);
#else
	lock (cache)
	{
		cache.Add(rc, mutatedRequestConfiguration);
	}
#endif

	// ReSharper restore InconsistentlySynchronizedField

	return;

	RequestConfiguration MutateRequestConfiguration(IRequestConfiguration requestConfiguration)
	{
		return new RequestConfiguration(requestConfiguration)
		{
			UserAgent = UserAgent.Create("my-custom-user-agent")
		};
	}
}

var client = new ElasticsearchClient(settings);

var s = settings.SourceSerializer.SerializeToString(new Person
{
	Q = new Query
	{
		MatchAll = new MatchAllQuery()
	}
});

await client.ExplainAsync("my-tweet-index", 1);
await client.DeleteAsync("my-tweet-index", 1);

BulkRequestDescriptor descriptor = new();
descriptor.Index("indexName");
