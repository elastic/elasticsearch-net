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
