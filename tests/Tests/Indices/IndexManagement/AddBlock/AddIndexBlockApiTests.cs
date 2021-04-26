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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.AddBlock
{
	[SkipVersion("<7.9.0", "indices add index introduced in 7.9.0")]
	public class AddIndexBlockApiTests
		: ApiIntegrationTestBase<WritableCluster, AddIndexBlockResponse, IAddIndexBlockRequest, AddIndexBlockDescriptor, AddIndexBlockRequest>
	{
		public AddIndexBlockApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var createIndexResponse = client.Indices.Create(value.Value, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
				);

				if (!createIndexResponse.IsValid)
					throw new Exception($"exception whilst setting up integration test: {createIndexResponse.DebugInformation}");
			}
		}

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<AddIndexBlockDescriptor, IAddIndexBlockRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override AddIndexBlockRequest Initializer => new AddIndexBlockRequest(CallIsolatedValue, IndexBlock.Write);

		protected override string UrlPath => $"/{CallIsolatedValue}/_block/write";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.AddBlock(CallIsolatedValue, IndexBlock.Write, f),
			(client, f) => client.Indices.AddBlockAsync(CallIsolatedValue, IndexBlock.Write, f),
			(client, r) => client.Indices.AddBlock(r),
			(client, r) => client.Indices.AddBlockAsync(r)
		);

		protected override AddIndexBlockDescriptor NewDescriptor() => new AddIndexBlockDescriptor(CallIsolatedValue, IndexBlock.Write);

		protected override void ExpectResponse(AddIndexBlockResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
			response.Indices.Should().HaveCount(1);
			var first = response.Indices.First();
			first.Name.Should().Be(CallIsolatedValue);
			first.Blocked.Should().BeTrue();
		}
	}
}
