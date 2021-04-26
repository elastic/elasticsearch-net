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

namespace Nest
{
	/// <summary>
	/// Pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
	/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
	/// used to change the configuration of the following task.
	/// </summary>
	[MapsApi("ccr.pause_follow.json")]
	[ReadAs(typeof(PauseFollowIndexRequest))]
	public partial interface IPauseFollowIndexRequest { }

	/// <inheritdoc cref="IPauseFollowIndexRequest"/>
	public partial class PauseFollowIndexRequest { }

	/// <inheritdoc cref="IPauseFollowIndexRequest"/>
	public partial class PauseFollowIndexDescriptor { }
}
