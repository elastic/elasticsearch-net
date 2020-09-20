// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication.Follow.ForgetFollowerIndex
{
	public class ForgetFollowerIndexApiTests : ApiTestBase<XPackCluster, ForgetFollowerIndexResponse, IForgetFollowerIndexRequest, ForgetFollowerIndexDescriptor, ForgetFollowerIndexRequest>
	{
		public ForgetFollowerIndexApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			follower_index = "follower-index",
			follower_cluster = "follower-cluster",
			leader_remote_cluster ="leader-remote-cluster",
			follower_index_uuid = "follower-index-uuid",
		};

		protected override ForgetFollowerIndexDescriptor NewDescriptor() => new ForgetFollowerIndexDescriptor("index");

		protected override Func<ForgetFollowerIndexDescriptor, IForgetFollowerIndexRequest> Fluent => d => d
			.Index("index")
			.FollowerIndex("follower-index")
			.FollowerCluster("follower-cluster")
			.LeaderRemoteCluster("leader-remote-cluster")
			.FollowerIndexUUID("follower-index-uuid")
		;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ForgetFollowerIndexRequest Initializer => new ForgetFollowerIndexRequest("index")
		{
			FollowerIndex = "follower-index",
			FollowerCluster = "follower-cluster",
			LeaderRemoteCluster ="leader-remote-cluster",
			FollowerIndexUUID = "follower-index-uuid",
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => "/index/_ccr/forget_follower";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CrossClusterReplication.ForgetFollowerIndex("index", f),
			(client, f) => client.CrossClusterReplication.ForgetFollowerIndexAsync("index", f),
			(client, r) => client.CrossClusterReplication.ForgetFollowerIndex(r),
			(client, r) => client.CrossClusterReplication.ForgetFollowerIndexAsync(r)
		);
	}
}
