using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A response of the configuration for machine learning jobs.
	/// </summary>
	public class GetJobsResponse : ResponseBase
	{
		/// <summary>
		/// The count of jobs
		/// </summary>
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		/// <summary>
		/// The configuration of machine learning jobs
		/// </summary>
		[DataMember(Name ="jobs")]
		public IReadOnlyCollection<Job> Jobs { get; internal set; } = EmptyReadOnly<Job>.Collection;
	}
}
