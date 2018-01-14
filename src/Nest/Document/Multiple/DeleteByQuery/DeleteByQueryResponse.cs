using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteByQueryResponse : IResponse
	{
		[JsonProperty("took")]
		long Took { get; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("slice_id")]
		int? SliceId { get; }

		[JsonProperty("deleted")]
		long Deleted { get; }

		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("version_conflicts")]
		long VersionConflicts { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		[JsonProperty("throttled_millis")]
		long ThrottledMilliseconds { get; }

		[JsonProperty("requests_per_second")]
		float RequestsPerSecond { get; }

		[JsonProperty("throttled_until_millis")]
		long ThrottledUntilMilliseconds { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }
	}

	public class DeleteByQueryResponse : ResponseBase, IDeleteByQueryResponse
	{
		public override bool IsValid => this.ApiCall?.HttpStatusCode == 200 || !this.Failures.HasAny();

		public long Took { get; internal set; }

		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public int? SliceId { get; internal set; }

		public long Deleted { get; internal set; }

		public long Batches { get; internal set; }

		public long VersionConflicts { get; internal set; }

		public long Noops { get; internal set; }

		public Retries Retries { get; internal set; }

		public long ThrottledMilliseconds { get; internal set; }

		public float RequestsPerSecond { get; internal set; }

		public long ThrottledUntilMilliseconds { get; internal set; }

		public long Total { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;
	}
}
