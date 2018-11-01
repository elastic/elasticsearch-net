using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateByQueryResponse : IResponse
	{
		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("requests_per_second")]
		float RequestsPerSecond { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		/// <summary>
		///     Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("updated")]
		long Updated { get; }

		[JsonProperty("version_conflicts")]
		long VersionConflicts { get; }
	}

	public class UpdateByQueryResponse : ResponseBase, IUpdateByQueryResponse
	{
		public long Batches { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;
		public override bool IsValid => ApiCall?.HttpStatusCode == 200 && !Failures.HasAny();

		public long Noops { get; internal set; }

		public float RequestsPerSecond { get; internal set; }

		public Retries Retries { get; internal set; }

		/// <summary>
		///     Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public long Took { get; internal set; }

		public long Total { get; internal set; }

		public long Updated { get; internal set; }

		public long VersionConflicts { get; internal set; }
	}
}
