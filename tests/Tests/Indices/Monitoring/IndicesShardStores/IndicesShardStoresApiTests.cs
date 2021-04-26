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
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.Monitoring.IndicesShardStores
{
	public class IndicesShardStoresApiTests
		: ApiIntegrationTestBase<WritableCluster, IndicesShardStoresResponse, IIndicesShardStoresRequest, IndicesShardStoresDescriptor,
			IndicesShardStoresRequest>
	{
		private static readonly string IndexWithUnassignedShards = "nest-" + RandomString();

		public IndicesShardStoresApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{IndexWithUnassignedShards}/_shard_stores?status=all";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.Indices.Create(IndexWithUnassignedShards, s => s
				.Settings(settings => settings
					.NumberOfShards(1)
					.NumberOfReplicas(2)
				)
			);
			client.Index(new IndexRequest<object>((IndexName)IndexWithUnassignedShards)
			{
				Document = new { x = 1 },
				Refresh = Refresh.True
			});
		}

		protected override IndicesShardStoresRequest Initializer =>
			new IndicesShardStoresRequest(IndexWithUnassignedShards)
			{
				Status = new[] { "all" }
			};
		protected override Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> Fluent => s => s
			.Status("all");

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ShardStores(IndexWithUnassignedShards, f),
			(client, f) => client.Indices.ShardStoresAsync(IndexWithUnassignedShards, f),
			(client, r) => client.Indices.ShardStores(r),
			(client, r) => client.Indices.ShardStoresAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Indices.Should().NotBeEmpty();
			var indicesShardStore = r.Indices[IndexWithUnassignedShards];
			indicesShardStore.Should().NotBeNull();
			indicesShardStore.Shards.Should().NotBeEmpty().And.ContainKey("0");
			var shardStoreWrapper = indicesShardStore.Shards["0"];
			shardStoreWrapper.Stores.Should().NotBeNullOrEmpty();

			var shardStore = shardStoreWrapper.Stores.First();
			shardStore.Id.Should().NotBeNullOrWhiteSpace();
			shardStore.Name.Should().NotBeNullOrWhiteSpace();
			shardStore.TransportAddress.Should().NotBeNullOrWhiteSpace();
			shardStore.LegacyVersion.Should().Be(null);
			shardStore.AllocationId.Should().NotBeNullOrWhiteSpace();
			shardStore.Allocation.Should().Be(ShardStoreAllocation.Primary);
		});
	}
}
