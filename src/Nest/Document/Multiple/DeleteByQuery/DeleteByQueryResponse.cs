using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteByQueryResponse : IResponse
	{
		[DataMember(Name ="batches")]
		long Batches { get; }

		[DataMember(Name ="deleted")]
		long Deleted { get; }

		[DataMember(Name ="failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[DataMember(Name ="noops")]
		long Noops { get; }

		[DataMember(Name ="requests_per_second")]
		float RequestsPerSecond { get; }

		[DataMember(Name ="retries")]
		Retries Retries { get; }

		[DataMember(Name ="slice_id")]
		int? SliceId { get; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[DataMember(Name ="task")]
		TaskId Task { get; }

		[DataMember(Name ="throttled_millis")]
		long ThrottledMilliseconds { get; }

		[DataMember(Name ="throttled_until_millis")]
		long ThrottledUntilMilliseconds { get; }

		[DataMember(Name ="timed_out")]
		bool TimedOut { get; }

		[DataMember(Name ="took")]
		long Took { get; }

		[DataMember(Name ="total")]
		long Total { get; }

		[DataMember(Name ="version_conflicts")]
		long VersionConflicts { get; }
	}

	public class DeleteByQueryResponse : ResponseBase, IDeleteByQueryResponse
	{
		public long Batches { get; internal set; }

		public long Deleted { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;
		public override bool IsValid => ApiCall?.HttpStatusCode == 200 || !Failures.HasAny();

		public long Noops { get; internal set; }

		public float RequestsPerSecond { get; internal set; }

		public Retries Retries { get; internal set; }

		public int? SliceId { get; internal set; }

		public TaskId Task { get; internal set; }

		public long ThrottledMilliseconds { get; internal set; }

		public long ThrottledUntilMilliseconds { get; internal set; }

		public bool TimedOut { get; internal set; }

		public long Took { get; internal set; }

		public long Total { get; internal set; }

		public long VersionConflicts { get; internal set; }
	}
}
