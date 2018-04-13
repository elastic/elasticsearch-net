using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IReindexOnServerResponse : IResponse
	{
		//https://github.com/elastic/elasticsearch/commit/11f90bffda50f0acc8dc1409f3f33005e1249234
		// 2.3 released this writing the time value's to string e.g 123.4ms instead of long as all the others took responses
		[JsonProperty("took")]
		Time Took { get; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("created")]
		long Created { get; }

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

		[JsonProperty("slice_id")]
		int? SliceId { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ReindexOnServerResponse : ResponseBase, IReindexOnServerResponse
	{
		public override bool IsValid => base.IsValid && !this.Failures.HasAny();

		public Time Took { get; internal set; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to <c>false</c> on the request
		/// </summary>
		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public long Total { get; internal set; }

		public long Created { get; internal set; }

		public long Updated { get; internal set; }

		public long Batches { get; internal set; }

		public long VersionConflicts { get; internal set; }

		public long Noops { get; internal set; }

		public Retries Retries { get; internal set; }

		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; } = EmptyReadOnly<BulkIndexByScrollFailure>.Collection;

		public int? SliceId { get; internal set; }
	}
}
