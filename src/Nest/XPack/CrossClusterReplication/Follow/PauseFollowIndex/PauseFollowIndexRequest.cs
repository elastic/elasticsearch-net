using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
	/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
	/// used to change the configuration of the following task.
	/// </summary>
	[MapsApi("ccr.pause_follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<PauseFollowIndexRequest>))]
	public partial interface IPauseFollowIndexRequest { }

	/// <inheritdoc cref="IPauseFollowIndexRequest"/>
	public partial class PauseFollowIndexRequest { }

	/// <inheritdoc cref="IPauseFollowIndexRequest"/>
	public partial class PauseFollowIndexDescriptor { }
}
