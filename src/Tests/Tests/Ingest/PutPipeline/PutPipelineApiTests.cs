using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.PutPipeline
{
	public class PutPipelineApiTests
		: ApiIntegrationTestBase<WritableCluster, PutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public PutPipelineApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			description = "My test pipeline",
			processors = ProcessorAssertions.AllAsJson
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d
			.Description("My test pipeline")
			.Processors(ProcessorAssertions.Fluent);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id)
		{
			Description = "My test pipeline",
			Processors = ProcessorAssertions.Initializers
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.PutPipeline(_id, f),
			(client, f) => client.Ingest.PutPipelineAsync(_id, f),
			(client, r) => client.Ingest.PutPipeline(r),
			(client, r) => client.Ingest.PutPipelineAsync(r)
		);

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override void ExpectResponse(PutPipelineResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
