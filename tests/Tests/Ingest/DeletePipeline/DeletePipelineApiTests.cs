// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.DeletePipeline
{
	//integration test is part of PipelineCrudTests
	public class DeletePipelineApiTests
		: ApiTestBase<ReadOnlyCluster, DeletePipelineResponse, IDeletePipelineRequest, DeletePipelineDescriptor, DeletePipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public DeletePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeletePipelineDescriptor, IDeletePipelineRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeletePipelineRequest Initializer => new DeletePipelineRequest(_id);
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.DeletePipeline(_id, f),
			(client, f) => client.Ingest.DeletePipelineAsync(_id, f),
			(client, r) => client.Ingest.DeletePipeline(r),
			(client, r) => client.Ingest.DeletePipelineAsync(r)
		);

		protected override DeletePipelineDescriptor NewDescriptor() => new DeletePipelineDescriptor(_id);
	}
}
