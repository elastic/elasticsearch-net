using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.IndexManagement.SplitIndex
{
	public class SplitIndexApiTests : ApiIntegrationTestBase<WritableCluster, ISplitIndexResponse, ISplitIndexRequest, SplitIndexDescriptor, SplitIndexRequest>
	{
		public SplitIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SplitIndex(CallIsolatedValue, CallIsolatedValue + "-target", f),
			fluentAsync: (client, f) => client.SplitIndexAsync(CallIsolatedValue, CallIsolatedValue + "-target", f),
			request: (client, r) => client.SplitIndex(r),
			requestAsync: (client, r) => client.SplitIndexAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var create = client.CreateIndex(CallIsolatedValue, c => c
				.Settings(s => s
					.NumberOfShards(4)
					.NumberOfRoutingShards(8)
					.NumberOfReplicas(0)
				)
			);
			create.ShouldBeValid();
			var update = client.UpdateIndexSettings(CallIsolatedValue, u => u
				.IndexSettings(s => s
					.BlocksWrite()
				)
			);
			update.ShouldBeValid();
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}/_split/{CallIsolatedValue}-target";

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_shards", 8 }
			}
		};

		protected override SplitIndexDescriptor NewDescriptor() => new SplitIndexDescriptor(CallIsolatedValue, CallIsolatedValue + "-target");

		protected override Func<SplitIndexDescriptor, ISplitIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfShards(8)
			);

		protected override SplitIndexRequest Initializer => new SplitIndexRequest(CallIsolatedValue, CallIsolatedValue + "-target")
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 8
			}
		};

		protected override void ExpectResponse(ISplitIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
		}
	}
}
