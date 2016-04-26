using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Ingest.SimulatePipeline
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SimulatePipelineApiTests : ApiTestBase<ISimulatePipelineResponse, ISimulatePipelineRequest, SimulatePipelineDescriptor, SimulatePipelineRequest>
	{
		public SimulatePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "existing-pipeline";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SimulatePipeline(f),
			fluentAsync: (client, f) => client.SimulatePipelineAsync(f),
			request: (client, r) => client.SimulatePipeline(r),
			requestAsync: (client, r) => client.SimulatePipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}/_simulate";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
		};

		protected override Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> Fluent => d => d
			.Id(_id);

		protected override SimulatePipelineRequest Initializer => new SimulatePipelineRequest(_id)
		{
		};
	}
}
