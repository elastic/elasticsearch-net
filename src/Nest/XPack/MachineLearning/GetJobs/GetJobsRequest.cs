namespace Nest
{
	/// <summary>
	/// Retrieve usage information for machine learning jobs.
	/// </summary>
	[MapsApi("ml.get_jobs.json")]
	public partial interface IGetJobsRequest { }

	/// <inheritdoc />
	public partial class GetJobsRequest { }

	/// <inheritdoc />
	public partial class GetJobsDescriptor { }
}
