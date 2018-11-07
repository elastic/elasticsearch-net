using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Ingest.PutPipeline
{
	public class PutPipelineApiTests
		: ApiIntegrationTestBase<WritableCluster, IPutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public PutPipelineApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			description = "My test pipeline",
			processors = ProcessorAssertions.Json
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d
			.Description("My test pipeline")
			.Processors(ProcessorAssertions.Fluent);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id)
		{
			Description = "My test pipeline",
			Processors = ProcessorAssertions.Initializer
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutPipeline(_id, f),
			(client, f) => client.PutPipelineAsync(_id, f),
			(client, r) => client.PutPipeline(r),
			(client, r) => client.PutPipelineAsync(r)
		);

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override void ExpectResponse(IPutPipelineResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
