// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Stops the following task associated with a follower index and removes index metadata and settings associated with
	/// cross-cluster replication. This enables the index to treated as a regular index. The follower index must be paused and closed
	/// before invoking the unfollow API.
	/// </summary>
	[MapsApi("ccr.unfollow.json")]
	[ReadAs(typeof(UnfollowIndexRequest))]
	public partial interface IUnfollowIndexRequest { }

	/// <inheritdoc cref="IUnfollowIndexRequest"/>
	public partial class UnfollowIndexRequest { }

	/// <inheritdoc cref="IUnfollowIndexRequest"/>
	public partial class UnfollowIndexDescriptor { }
}
