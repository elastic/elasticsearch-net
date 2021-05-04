// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
