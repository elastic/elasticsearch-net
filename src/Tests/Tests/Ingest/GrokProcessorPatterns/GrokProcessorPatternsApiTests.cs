using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Ingest.Processor
{
	public class GrokProcessorPatternsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGrokProcessorPatternsResponse, IGrokProcessorPatternsRequest, GrokProcessorPatternsDescriptor,
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
			(client, f) => client.GrokProcessorPatterns(f),
			(client, f) => client.GrokProcessorPatternsAsync(f),
			(client, r) => client.GrokProcessorPatterns(r),
			(client, r) => client.GrokProcessorPatternsAsync(r)
		);
	}
}
