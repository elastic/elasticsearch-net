using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IReindexOnServerResponse : IResponse
	{
		[JsonProperty("batches")]
		long Batches { get; }

		[JsonProperty("created")]
		long Created { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[JsonProperty("noops")]
		long Noops { get; }

		[JsonProperty("retries")]
		Retries Retries { get; }

		[JsonProperty("slice_id")]
		int? SliceId { get; }

		/// <summary>
		///     Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		//https://github.com/elastic/elasticsearch/commit/11f90bffda50f0acc8dc1409f3f33005e1249234
		// 2.3 released this writing the time value's to string e.g 123.4ms instead of long as all the others took responses
		[JsonProperty("took")]
		Time Took { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("updated")]
		long Updated { get; }

		[JsonProperty("version_conflicts")]
		long VersionConflicts { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ReindexOnServerResponse : ResponseBase, IReindexOnServerResponse
	{
		public long Batches { get; internal set; }

		public long Created { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;
		public override bool IsValid => base.IsValid && !Failures.HasAny();

		public long Noops { get; internal set; }

		public Retries Retries { get; internal set; }

		public int? SliceId { get; internal set; }

		/// <summary>
		///     Only has a value if WaitForCompletion is set to <c>false</c> on the request
		/// </summary>
		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public Time Took { get; internal set; }

		public long Total { get; internal set; }

		public long Updated { get; internal set; }

		public long VersionConflicts { get; internal set; }
	}
}
