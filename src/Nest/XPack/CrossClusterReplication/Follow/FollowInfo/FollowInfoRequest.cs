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
