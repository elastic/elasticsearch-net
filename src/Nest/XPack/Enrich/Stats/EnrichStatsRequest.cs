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
