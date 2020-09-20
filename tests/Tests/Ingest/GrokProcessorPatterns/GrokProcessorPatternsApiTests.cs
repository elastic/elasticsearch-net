// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.GrokProcessorPatterns
{
	public class GrokProcessorPatternsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GrokProcessorPatternsResponse, IGrokProcessorPatternsRequest, GrokProcessorPatternsDescriptor,
			GrokProcessorPatternsRequest>
	{
		public GrokProcessorPatternsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GrokProcessorPatternsRequest Initializer => new GrokProcessorPatternsRequest();
		protected override string UrlPath => $"/_ingest/processor/grok";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.GrokProcessorPatterns(f),
			(client, f) => client.Ingest.GrokProcessorPatternsAsync(f),
			(client, r) => client.Ingest.GrokProcessorPatterns(r),
			(client, r) => client.Ingest.GrokProcessorPatternsAsync(r)
		);
	}
}
