/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
