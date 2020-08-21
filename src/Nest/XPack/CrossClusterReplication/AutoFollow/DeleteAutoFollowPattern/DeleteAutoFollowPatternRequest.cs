// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
