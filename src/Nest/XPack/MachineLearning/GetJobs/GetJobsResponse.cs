using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A response of the configuration for machine learning jobs.
	/// </summary>
	public interface IGetJobsResponse : IResponse
	{
		/// <summary>
		/// The count of jobs
		/// </summary>
		[JsonProperty("count")]
		long Count { get; }

		/// <summary>
		/// The configuration of machine learning jobs
		/// </summary>
		[JsonProperty("jobs")]
		IReadOnlyCollection<Job> Jobs { get; }
	}

	/// <inheritdoc />
	public class GetJobsResponse : ResponseBase, IGetJobsResponse
	{
		/// <inheritdoc />
		public long Count { get; internal set; }

		/// <inheritdoc />
		public IReadOnlyCollection<Job> Jobs { get; internal set; } = EmptyReadOnly<Job>.Collection;
	}
}
