using System.Collections.Generic;
using System.Runtime.Serialization;

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
		[DataMember(Name ="count")]
		long Count { get; }

		/// <summary>
		/// The configuration of machine learning jobs
		/// </summary>
		[DataMember(Name ="jobs")]
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
