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

namespace Tests.Cluster.ClusterSettings.ClusterPutSettings
{
	public class ClusterPutSettingsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ClusterPutSettingsResponse, IClusterPutSettingsRequest, ClusterPutSettingsDescriptor,
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
			(client, f) => client.Cluster.PutSettings(f),
			(client, f) => client.Cluster.PutSettingsAsync(f),
			(client, r) => client.Cluster.PutSettings(r),
			(client, r) => client.Cluster.PutSettingsAsync(r)
		);

		protected override void ExpectResponse(ClusterPutSettingsResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.Transient.Should().HaveCount(1);
		}
	}

	public class ClusterPutSettingsNoopApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ClusterPutSettingsResponse, IClusterPutSettingsRequest, ClusterPutSettingsDescriptor,
			ClusterPutSettingsRequest>
	{
		public ClusterPutSettingsNoopApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override int ExpectStatusCode => 400;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ClusterPutSettingsRequest Initializer => new ClusterPutSettingsRequest();

		protected override string UrlPath => "/_cluster/settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.PutSettings(f),
			(client, f) => client.Cluster.PutSettingsAsync(f),
			(client, r) => client.Cluster.PutSettings(r),
			(client, r) => client.Cluster.PutSettingsAsync(r)
		);

		protected override void ExpectResponse(ClusterPutSettingsResponse response)
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
