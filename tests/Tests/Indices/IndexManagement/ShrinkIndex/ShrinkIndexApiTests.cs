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

namespace Tests.Indices.IndexManagement.ShrinkIndex
{
	public class ShrinkIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, ShrinkIndexResponse, IShrinkIndexRequest, ShrinkIndexDescriptor, ShrinkIndexRequest>
	{
		public ShrinkIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_shards", 4 }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ShrinkIndexDescriptor, IShrinkIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfShards(4)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ShrinkIndexRequest Initializer => new ShrinkIndexRequest(CallIsolatedValue, CallIsolatedValue + "-target")
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 4
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_shrink/{CallIsolatedValue}-target";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Shrink(CallIsolatedValue, CallIsolatedValue + "-target", f),
			(client, f) => client.Indices.ShrinkAsync(CallIsolatedValue, CallIsolatedValue + "-target", f),
			(client, r) => client.Indices.Shrink(r),
			(client, r) => client.Indices.ShrinkAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var create = client.Indices.Create(CallIsolatedValue, c => c
				.Settings(s => s
					.NumberOfShards(8)
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

		protected override ShrinkIndexDescriptor NewDescriptor() => new ShrinkIndexDescriptor(CallIsolatedValue, CallIsolatedValue + "-target");

		protected override void ExpectResponse(ShrinkIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
		}
	}
}
