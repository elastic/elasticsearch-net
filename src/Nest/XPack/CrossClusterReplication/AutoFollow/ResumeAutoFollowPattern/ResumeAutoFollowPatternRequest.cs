// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
