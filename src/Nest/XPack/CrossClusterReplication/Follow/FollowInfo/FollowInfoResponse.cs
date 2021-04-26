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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class FollowInfoResponse : ResponseBase
	{
		[DataMember(Name = "follower_indices")]
		public IReadOnlyCollection<FollowerInfo> FollowerIndices { get; internal set; } = EmptyReadOnly<FollowerInfo>.Collection;
	}

	public class FollowerInfo
	{
		[DataMember(Name = "follower_index")]
		public string FollowerIndex { get; internal set; }

		[DataMember(Name = "remote_cluster")]
		public string RemoteCluster { get; internal set; }

		[DataMember(Name = "leader_index")]
		public string LeaderIndex { get; internal set; }

		[DataMember(Name = "status")]
		public FollowerIndexStatus Status { get; internal set; }

		[DataMember(Name = "parameters")]
		public FollowConfig Parameters { get; internal set; }
	}
}
