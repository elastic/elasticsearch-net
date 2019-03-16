using System;

namespace Nest
{
	/// <summary> Deletes a configured collection of auto-follow patterns. </summary>
	[MapsApi("ccr.delete_auto_follow_pattern.json")]
	[ReadAs(typeof(DeleteAutoFollowPatternRequest))]
	public partial interface IDeleteAutoFollowPatternRequest { }

	/// <inheritdoc cref="IDeleteAutoFollowPatternRequest"/>
	public partial class DeleteAutoFollowPatternRequest { }

	/// <inheritdoc cref="IDeleteAutoFollowPatternRequest"/>
	public partial class DeleteAutoFollowPatternDescriptor { }
}
