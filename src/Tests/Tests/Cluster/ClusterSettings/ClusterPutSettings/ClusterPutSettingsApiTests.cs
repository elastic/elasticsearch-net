using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.ClusterSettings.ClusterPutSettings
{
	public class ClusterPutSettingsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IClusterPutSettingsResponse, IClusterPutSettingsRequest, ClusterPutSettingsDescriptor,
			ClusterPutSettingsRequest>
	{
		public ClusterPutSettingsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> Fluent => c => c
			.Transient(s => s
				.Add("indices.recovery.max_bytes_per_sec", "41mb")
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ClusterPutSettingsRequest Initializer => new ClusterPutSettingsRequest
		{
			Transient = new Dictionary<string, object>
			{
				{ "indices.recovery.max_bytes_per_sec", "41mb" }
			}
		};

		protected override string UrlPath => "/_cluster/settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterPutSettings(f),
			(client, f) => client.ClusterPutSettingsAsync(f),
			(client, r) => client.ClusterPutSettings(r),
			(client, r) => client.ClusterPutSettingsAsync(r)
		);

		protected override void ExpectResponse(IClusterPutSettingsResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.Transient.Should().HaveCount(1);
		}
	}

	public class ClusterPutSettingsNoopApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IClusterPutSettingsResponse, IClusterPutSettingsRequest, ClusterPutSettingsDescriptor,
			ClusterPutSettingsRequest>
	{
		public ClusterPutSettingsNoopApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override int ExpectStatusCode => 400;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ClusterPutSettingsRequest Initializer => new ClusterPutSettingsRequest
			{ };

		protected override string UrlPath => "/_cluster/settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterPutSettings(f),
			(client, f) => client.ClusterPutSettingsAsync(f),
			(client, r) => client.ClusterPutSettings(r),
			(client, r) => client.ClusterPutSettingsAsync(r)
		);

		protected override void ExpectResponse(IClusterPutSettingsResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Contain("no settings to update");
			response.ServerError.Error.Type.Should().Contain("action_request_validation_exception");
		}
	}
}
