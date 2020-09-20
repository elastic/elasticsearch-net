// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
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
