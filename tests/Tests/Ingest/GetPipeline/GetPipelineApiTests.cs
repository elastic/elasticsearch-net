// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.GetPipeline
{
	//integration test is part of PipelineCrudTests
	public class GetPipelineApiTests
		: ApiTestBase<ReadOnlyCluster, GetPipelineResponse, IGetPipelineRequest, GetPipelineDescriptor, GetPipelineRequest>
	{
		private const string PipelineId = "pipeline-1";

		public GetPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetPipelineDescriptor, IGetPipelineRequest> Fluent => d => d.Id(PipelineId);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetPipelineRequest Initializer => new GetPipelineRequest(PipelineId);
		protected override string UrlPath => $"/_ingest/pipeline/{PipelineId}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.GetPipeline(f),
			(client, f) => client.Ingest.GetPipelineAsync(f),
			(client, r) => client.Ingest.GetPipeline(r),
			(client, r) => client.Ingest.GetPipelineAsync(r)
		);

		protected override GetPipelineDescriptor NewDescriptor() => new GetPipelineDescriptor().Id(PipelineId);
	}
}
