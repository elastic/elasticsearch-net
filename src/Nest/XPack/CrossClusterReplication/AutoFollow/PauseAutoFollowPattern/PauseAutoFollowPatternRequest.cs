// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
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
