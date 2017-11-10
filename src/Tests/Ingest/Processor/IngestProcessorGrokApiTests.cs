using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Ingest.IngestProcessorGrok
{
	public class IngestProcessorGrokApiTests : ApiTestBase<ReadOnlyCluster, IIngestProcessorGrokResponse, IIngestProcessorGrokRequest, IngestProcessorGrokDescriptor, IngestProcessorGrokRequest>
	{
		public IngestProcessorGrokApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IngestProcessorGrok(f),
			fluentAsync: (client, f) => client.IngestProcessorGrokAsync(f),
			request: (client, r) => client.IngestProcessorGrok(r),
			requestAsync: (client, r) => client.IngestProcessorGrokAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_ingest/processor/grok";

		protected override Func<IngestProcessorGrokDescriptor, IIngestProcessorGrokRequest> Fluent => d => d;

		protected override IngestProcessorGrokRequest Initializer => new IngestProcessorGrokRequest();
	}
}
