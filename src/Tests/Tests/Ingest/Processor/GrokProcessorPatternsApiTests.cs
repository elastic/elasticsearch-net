using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Ingest.Processor
{
	public class GrokProcessorPatternsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGrokProcessorPatternsResponse, IGrokProcessorPatternsRequest, GrokProcessorPatternsDescriptor, GrokProcessorPatternsRequest>
	{
		public GrokProcessorPatternsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GrokProcessorPatterns(f),
			fluentAsync: (client, f) => client.GrokProcessorPatternsAsync(f),
			request: (client, r) => client.GrokProcessorPatterns(r),
			requestAsync: (client, r) => client.GrokProcessorPatternsAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_ingest/processor/grok";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> Fluent => d => d;

		protected override GrokProcessorPatternsRequest Initializer => new GrokProcessorPatternsRequest();
	}
}
