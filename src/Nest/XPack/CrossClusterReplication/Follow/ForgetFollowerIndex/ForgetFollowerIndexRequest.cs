// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ccr.forget_follower.json")]
	[ReadAs(typeof(ForgetFollowerIndexRequest))]
	public partial interface IForgetFollowerIndexRequest
	{
		/// <summary>
		/// The name of the cluster containing the follower index.
		/// </summary>
		[DataMember(Name = "follower_cluster")]
		string FollowerCluster { get; set; }

		/// <summary>
		/// The name of the follower index.
		/// </summary>
		[DataMember(Name = "follower_index")]
		IndexName FollowerIndex { get; set; }

		/// <summary>
		/// The UUID of the follower index.
		/// </summary>
		[DataMember(Name = "follower_index_uuid")]
		string FollowerIndexUUID { get; set; }

		/// <summary>
		/// The alias (from the perspective of the cluster containing the follower index) of the remote cluster containing the leader index.
		/// </summary>
		[DataMember(Name = "leader_remote_cluster")]
		string LeaderRemoteCluster { get; set; }
	}

	/// <inheritdoc cref="IForgetFollowerIndexRequest"/>
	public partial class ForgetFollowerIndexRequest
	{
		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerCluster"/>
		public string FollowerCluster { get; set; }

		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerIndex"/>
		public IndexName FollowerIndex { get; set; }

		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerIndexUUID"/>
		public string FollowerIndexUUID { get; set; }

		/// <inheritdoc cref="IForgetFollowerIndexRequest.LeaderRemoteCluster"/>
		public string LeaderRemoteCluster { get; set; }
	}

	/// <inheritdoc cref="IForgetFollowerIndexRequest"/>
	public partial class ForgetFollowerIndexDescriptor
	{
		string IForgetFollowerIndexRequest.FollowerCluster { get; set; }
		IndexName IForgetFollowerIndexRequest.FollowerIndex { get; set; }
		string IForgetFollowerIndexRequest.FollowerIndexUUID { get; set; }
		string IForgetFollowerIndexRequest.LeaderRemoteCluster { get; set; }

		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerCluster"/>
		public ForgetFollowerIndexDescriptor FollowerCluster(string followerCluster) =>
			Assign(followerCluster, (a, v) => a.FollowerCluster = v);

		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerIndex"/>
		public ForgetFollowerIndexDescriptor FollowerIndex(IndexName followerIndex) =>
			Assign(followerIndex, (a, v) => a.FollowerIndex = v);

		/// <inheritdoc cref="IForgetFollowerIndexRequest.FollowerIndexUUID"/>
		public ForgetFollowerIndexDescriptor FollowerIndexUUID(string followerIndexUUID) =>
			Assign(followerIndexUUID, (a, v) => a.FollowerIndexUUID = v);

		/// <inheritdoc cref="IForgetFollowerIndexRequest.LeaderRemoteCluster"/>
		public ForgetFollowerIndexDescriptor LeaderRemoteCluster(string leaderRemoteCluster) =>
			Assign(leaderRemoteCluster, (a, v) => a.LeaderRemoteCluster = v);
	}
}
