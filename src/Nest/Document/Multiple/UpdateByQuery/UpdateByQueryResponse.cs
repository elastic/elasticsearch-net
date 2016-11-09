using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateByQueryResponse : IResponse
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

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("updated")]
		long Updated { get; }

		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("version_conflicts")]
		long VersionConflicts { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[JsonProperty("requests_per_second")]
		float RequestsPerSecond { get; }
	}

	public class UpdateByQueryResponse : ResponseBase, IUpdateByQueryResponse
	{
		public override bool IsValid => this.ApiCall?.HttpStatusCode == 200 && !this.Failures.HasAny();

		public long Took { get; internal set; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public long Total { get; internal set; }

		public long Updated { get; internal set; }

		public long Batches { get; internal set; }

		public long VersionConflicts { get; internal set; }

		public long Noops { get; internal set; }

		public Retries Retries { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;

		public float RequestsPerSecond { get; internal set; }
	}
}
