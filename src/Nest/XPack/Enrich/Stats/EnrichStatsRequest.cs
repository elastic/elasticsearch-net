// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// A request to get statistics about enrich
	/// </summary>
	[MapsApi("enrich.stats")]
	[ReadAs(typeof(EnrichStatsRequest))]
	public partial interface IEnrichStatsRequest
	{
	}

	/// <inheritdoc cref="IEnrichStatsRequest"/>
	public partial class EnrichStatsRequest : IEnrichStatsRequest
	{
	}

	/// <inheritdoc cref="IEnrichStatsRequest"/>
	public partial class EnrichStatsDescriptor
	{
	}
}
