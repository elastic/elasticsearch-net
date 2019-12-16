namespace Nest
{
	/// <summary>Resumes configured auto-follow patterns.</summary>
	[MapsApi("ccr.resume_auto_follow_pattern.json")]
	[ReadAs(typeof(ResumeAutoFollowPatternRequest))]
	public partial interface IResumeAutoFollowPatternRequest { }

	/// <inheritdoc cref="IResumeAutoFollowPatternRequest"/>
	public partial class ResumeAutoFollowPatternRequest { }

	/// <inheritdoc cref="IResumeAutoFollowPatternRequest"/>
	public partial class ResumeAutoFollowPatternDescriptor { }
}
