namespace Nest
{
	/// <summary>Pauses configured auto-follow patterns.</summary>
	[MapsApi("ccr.pause_auto_follow_pattern.json")]
	[ReadAs(typeof(PauseAutoFollowPatternRequest))]
	public partial interface IPauseAutoFollowPatternRequest { }

	/// <inheritdoc cref="IPauseAutoFollowPatternRequest"/>
	public partial class PauseAutoFollowPatternRequest { }

	/// <inheritdoc cref="IPauseAutoFollowPatternRequest"/>
	public partial class PauseAutoFollowPatternDescriptor { }
}
