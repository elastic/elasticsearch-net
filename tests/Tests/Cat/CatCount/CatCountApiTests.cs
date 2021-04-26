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
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatCount
{
	public class CatCountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Count(),
			(client, f) => client.Cat.CountAsync(),
			(client, r) => client.Cat.Count(r),
			(client, r) => client.Cat.CountAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatCountRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
	}

	public class CatCountSingleIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountSingleIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Count(c => c.Index<Project>()),
			(client, f) => client.Cat.CountAsync(c => c.Index<Project>()),
			(client, r) => client.Cat.Count(new CatCountRequest(typeof(Project))),
			(client, r) => client.Cat.CountAsync(new CatCountRequest(typeof(Project)))
		);

		protected override void ExpectResponse(CatResponse<CatCountRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
	}

	public class CatCountNonExistentIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountNonExistentIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count/non-existent-index";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Count(c => c.Index("non-existent-index")),
			(client, f) => client.Cat.CountAsync(c => c.Index("non-existent-index")),
			(client, r) => client.Cat.Count(new CatCountRequest("non-existent-index")),
			(client, r) => client.Cat.CountAsync(new CatCountRequest("non-existent-index"))
		);

		protected override void ExpectResponse(CatResponse<CatCountRecord> response)
		{
			response.ApiCall.Success.Should().BeTrue();
			response.ServerError.Should().NotBeNull();
			response.Records.Should().NotBeNull().And.BeEmpty();
		}
	}
}
