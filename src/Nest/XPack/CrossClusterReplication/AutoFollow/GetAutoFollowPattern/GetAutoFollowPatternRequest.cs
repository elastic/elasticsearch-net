// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary> Gets configured auto-follow patterns. This API will return the specified auto-follow pattern collection. </summary>
	[MapsApi("ccr.get_auto_follow_pattern.json")]
	[ReadAs(typeof(GetAutoFollowPatternRequest))]
	public partial interface IGetAutoFollowPatternRequest { }

	/// <inheritdoc cref="IGetAutoFollowPatternRequest"/>
	public partial class GetAutoFollowPatternRequest { }

	/// <inheritdoc cref="IGetAutoFollowPatternRequest"/>
	public partial class GetAutoFollowPatternDescriptor { }
}
