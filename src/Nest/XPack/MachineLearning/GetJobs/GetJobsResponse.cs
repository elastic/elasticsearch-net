// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

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
