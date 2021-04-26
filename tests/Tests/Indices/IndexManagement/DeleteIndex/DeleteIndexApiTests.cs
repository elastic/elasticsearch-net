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

using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	public class DeleteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, DeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Delete(CallIsolatedValue),
			(client, f) => client.Indices.DeleteAsync(CallIsolatedValue),
			(client, r) => client.Indices.Delete(r),
			(client, r) => client.Indices.DeleteAsync(r)
		);

		protected override void ExpectResponse(DeleteIndexResponse response) => response.Acknowledged.Should().BeTrue();
	}

	public class DeleteNonExistentIndexApiTests
		: ApiIntegrationTestBase
			<WritableCluster, DeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteNonExistentIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Delete(CallIsolatedValue),
			(client, f) => client.Indices.DeleteAsync(CallIsolatedValue),
			(client, r) => client.Indices.Delete(r),
			(client, r) => client.Indices.DeleteAsync(r)
		);

		protected override void ExpectResponse(DeleteIndexResponse response)
		{
			response.Acknowledged.Should().BeFalse();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.Error.Reason.Should().StartWith("no such index");
		}
	}

	public class DeleteAllIndicesApiTests
		: ApiTestBase<WritableCluster, DeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteAllIndicesApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(AllIndices);
		protected override string UrlPath => $"/_all";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Delete(AllIndices),
			(client, f) => client.Indices.DeleteAsync(AllIndices),
			(client, r) => client.Indices.Delete(r),
			(client, r) => client.Indices.DeleteAsync(r)
		);
	}
}
