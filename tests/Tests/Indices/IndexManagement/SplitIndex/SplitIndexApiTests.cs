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
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.SplitIndex
{
	public class SplitIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, SplitIndexResponse, ISplitIndexRequest, SplitIndexDescriptor, SplitIndexRequest>
	{
		public SplitIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_shards", 8 }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SplitIndexDescriptor, ISplitIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfShards(8)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override SplitIndexRequest Initializer => new SplitIndexRequest(CallIsolatedValue, CallIsolatedValue + "-target")
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 8
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_split/{CallIsolatedValue}-target";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Split(CallIsolatedValue, CallIsolatedValue + "-target", f),
			(client, f) => client.Indices.SplitAsync(CallIsolatedValue, CallIsolatedValue + "-target", f),
			(client, r) => client.Indices.Split(r),
			(client, r) => client.Indices.SplitAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var create = client.Indices.Create(CallIsolatedValue, c => c
				.Settings(s => s
					.NumberOfShards(4)
					.NumberOfRoutingShards(8)
					.NumberOfReplicas(0)
				)
			);
			create.ShouldBeValid();
			var update = client.Indices.UpdateSettings(CallIsolatedValue, u => u
				.IndexSettings(s => s
					.BlocksWrite()
				)
			);
			update.ShouldBeValid();
		}

		protected override SplitIndexDescriptor NewDescriptor() => new SplitIndexDescriptor(CallIsolatedValue, CallIsolatedValue + "-target");

		protected override void ExpectResponse(SplitIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
		}
	}
}
