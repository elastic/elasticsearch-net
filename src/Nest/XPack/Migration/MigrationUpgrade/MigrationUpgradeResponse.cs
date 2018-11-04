using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMigrationUpgradeResponse : IResponse
	{
		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("created")]
		long Created { get; }

		[JsonProperty("deleted")]
		long Deleted { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		/// <summary>
		/// The id of the task if <see cref="MigrationUpgradeRequestParameters.WaitForCompletion" />
		/// is set to <c>false</c> on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("throttled_millis")]
		long ThrottledMilliseconds { get; }

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

	public class MigrationUpgradeResponse : ResponseBase, IMigrationUpgradeResponse
	{
		public long Batches { get; internal set; }
		public long Created { get; internal set; }
		public long Deleted { get; internal set; }
		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; }
		public override bool IsValid => ApiCall?.HttpStatusCode == 200 && !Failures.HasAny();
		public long Noops { get; internal set; }
		public Retries Retries { get; internal set; }

		public TaskId Task { get; internal set; }
		public long ThrottledMilliseconds { get; internal set; }
		public bool TimedOut { get; internal set; }
		public long Took { get; internal set; }
		public long Total { get; internal set; }
		public long Updated { get; internal set; }
		public long VersionConflicts { get; internal set; }
	}
}
