using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Ingest.GrokProcessorPatterns
{
	public class GrokProcessorPatternsApiTests : ApiTestBase<ReadOnlyCluster, IGrokProcessorPatternsResponse, IGrokProcessorPatternsRequest, GrokProcessorPatternsDescriptor, GrokProcessorPatternsRequest>
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

		protected override Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> Fluent => d => d;

		protected override GrokProcessorPatternsRequest Initializer => new GrokProcessorPatternsRequest();
	}
}
