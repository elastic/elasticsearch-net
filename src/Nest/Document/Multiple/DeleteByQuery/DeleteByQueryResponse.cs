// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteByQueryResponse : ResponseBase
	{
		public override bool IsValid => ApiCall?.HttpStatusCode == 200 || !Failures.HasAny();

		[DataMember(Name ="batches")]
		public long Batches { get; internal set; }

		[DataMember(Name ="deleted")]
		public long Deleted { get; internal set; }

		[DataMember(Name ="failures")]
		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; }
			= EmptyReadOnly<BulkIndexByScrollFailure>.Collection;

		[DataMember(Name ="noops")]
		public long Noops { get; internal set; }

		[DataMember(Name ="requests_per_second")]
		public float RequestsPerSecond { get; internal set; }

		[DataMember(Name ="retries")]
		public Retries Retries { get; internal set; }

		[DataMember(Name ="slice_id")]
		public int? SliceId { get; internal set; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[DataMember(Name ="task")]
		public TaskId Task { get; internal set; }

		[DataMember(Name ="throttled_millis")]
		public long ThrottledMilliseconds { get; internal set; }

		[DataMember(Name ="throttled_until_millis")]
		public long ThrottledUntilMilliseconds { get; internal set; }

		[DataMember(Name ="timed_out")]
		public bool TimedOut { get; internal set; }

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}
}
