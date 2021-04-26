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
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatShards
{
	public class CatShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatShardsRecord>, ICatShardsRequest, CatShardsDescriptor, CatShardsRequest>
	{
		public CatShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/shards";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Shards(),
			(client, f) => client.Cat.ShardsAsync(),
			(client, r) => client.Cat.Shards(r),
			(client, r) => client.Cat.ShardsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatShardsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.PrimaryOrReplica));
	}
}
