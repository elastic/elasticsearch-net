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

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		private static readonly IndexName ProjectIndex = Index<Project>();

		public GetIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetIndexRequest Initializer => new GetIndexRequest(ProjectIndex);
		protected override string UrlPath => $"/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Get(typeof(Project)),
			(client, f) => client.Indices.GetAsync(typeof(Project)),
			(client, r) => client.Indices.Get(r),
			(client, r) => client.Indices.GetAsync(r)
		);

		protected override void ExpectResponse(GetIndexResponse response)
		{
			response.Indices.Should().NotBeNull();
			response.Indices.Count.Should().BeGreaterThan(0);
			var projectIndex = response.Indices[ProjectIndex];
			projectIndex.Should().NotBeNull();
		}
	}


	public class GetAllIndicesApiTests
		: ApiTestBase<ReadOnlyCluster, GetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		public GetAllIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetIndexRequest Initializer => new GetIndexRequest(AllIndices);
		protected override string UrlPath => $"/_all";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Get(AllIndices),
			(client, f) => client.Indices.GetAsync(AllIndices),
			(client, r) => client.Indices.Get(r),
			(client, r) => client.Indices.GetAsync(r)
		);
	}
}
