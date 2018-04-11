using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IMigrationUpgradeResponse : IResponse
	{
		/// <summary>
		/// The id of the task if <see cref="MigrationUpgradeRequestParameters.WaitForCompletion"/>
		/// is set to <c>false</c> on the request
		/// </summary>
		[JsonProperty("task")]
		TaskId Task { get; }

		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("total")]
		long Total { get; }

		[JsonProperty("updated")]
		long Updated { get; }

		[JsonProperty("created")]
		long Created { get; }

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

		[JsonProperty("failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }
	}

	public class MigrationUpgradeResponse : ResponseBase, IMigrationUpgradeResponse
	{
		public override bool IsValid => this.ApiCall?.HttpStatusCode == 200 && !this.Failures.HasAny();

		public TaskId Task { get; internal set; }
		public long Took { get; internal set; }
		public bool TimedOut { get; internal set; }
		public long Total { get; internal set; }
		public long Updated { get; internal set; }
		public long Created { get; internal set; }
		public long Deleted { get; internal set; }
		public long Batches { get; internal set; }
		public long VersionConflicts { get; internal set; }
		public long Noops { get; internal set; }
		public Retries Retries { get; internal set; }
		public long ThrottledMilliseconds { get; internal set; }
		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; }
	}
}
