using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteByQueryResponse : IResponse
	{
		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("deleted")]
		long Deleted { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("requests_per_second")]
		float RequestsPerSecond { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		[JsonProperty("slice_id")]
		int? SliceId { get; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("throttled_millis")]
		long ThrottledMilliseconds { get; }

		[JsonProperty("throttled_until_millis")]
		long ThrottledUntilMilliseconds { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("version_conflicts")]
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
