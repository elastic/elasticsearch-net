using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API stops the following task associated with a follower index and removes index metadata and settings associated with
	/// cross-cluster replication. This enables the index to treated as a regular index. The follower index must be paused and closed
	/// before invoking the unfollow API.
	/// </summary>
	[MapsApi("ccr.unfollow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<UnfollowIndexRequest>))]
	public partial interface IUnfollowIndexRequest { }

	/// <inheritdoc cref="IUnfollowIndexRequest"/>
	public partial class UnfollowIndexRequest { }

	/// <inheritdoc cref="IUnfollowIndexRequest"/>
	public partial class UnfollowIndexDescriptor { }
}
