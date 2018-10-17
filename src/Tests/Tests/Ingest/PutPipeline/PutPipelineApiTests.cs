using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Ingest.PutPipeline
{
	public class PutPipelineApiTests
		: ApiIntegrationTestBase<WritableCluster, IPutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		public PutPipelineApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutPipeline(_id, f),
			fluentAsync: (client, f) => client.PutPipelineAsync(_id, f),
			request: (client, r) => client.PutPipeline(r),
			requestAsync: (client, r) => client.PutPipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";
		protected override bool SupportsDeserialization => false;
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			description = "My test pipeline",
			processors = ProcessorAssertions.Json
		};

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d
			.Description("My test pipeline")
			.Processors(ProcessorAssertions.Fluent);

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id)
		{
			Description = "My test pipeline",
			Processors = ProcessorAssertions.Initializer
		};

		protected override void ExpectResponse(IPutPipelineResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
