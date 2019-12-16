using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Deletes an enrich policy
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
