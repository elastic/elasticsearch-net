// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Retrieves information about all follower indices.
	/// </summary>
	[MapsApi("ccr.follow_info.json")]
	public partial interface IFollowInfoRequest { }

	/// <inheritdoc cref="IFollowInfoRequest" />
	public partial class FollowInfoRequest { }

	/// <inheritdoc cref="IFollowInfoRequest" />
	public partial class FollowInfoDescriptor { }
}
