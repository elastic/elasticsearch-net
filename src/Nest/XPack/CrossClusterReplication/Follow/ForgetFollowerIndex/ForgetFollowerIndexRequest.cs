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
