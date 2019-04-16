namespace Nest
{
	/// <summary>
	/// Retrieve usage information for machine learning jobs.
	/// </summary>
	[MapsApi("ml.get_job_stats.json")]
	public partial interface IGetJobStatsRequest { }

	/// <inheritdoc />
	public partial class GetJobStatsRequest { }

	/// <inheritdoc />
	public partial class GetJobStatsDescriptor { }
}
