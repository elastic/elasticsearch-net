using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Elasticsearch.Net.Specification.MigrationApi;

namespace Nest
{
	public class MigrationUpgradeResponse : ResponseBase
	{
		public override bool IsValid => ApiCall?.HttpStatusCode == 200 && !Failures.HasAny();

		[DataMember(Name ="batches")]
		public long Batches { get; internal set; }

		[DataMember(Name ="created")]
		public long Created { get; internal set; }

		[DataMember(Name ="deleted")]
		public long Deleted { get; internal set; }

		[DataMember(Name = "failures")]
		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; }
			= EmptyReadOnly<BulkIndexByScrollFailure>.Collection;

		[DataMember(Name ="noops")]
		public long Noops { get; internal set; }

		[DataMember(Name ="retries")]
		public Retries Retries { get; internal set; }

		/// <summary>
		/// The id of the task if <see cref="MigrationUpgradeRequestParameters.WaitForCompletion" />
		/// is set to <c>false</c> on the request
		/// </summary>
		[DataMember(Name ="task")]
		public TaskId Task { get; internal set; }

		[DataMember(Name ="throttled_millis")]
		public long ThrottledMilliseconds { get; internal set; }

		[DataMember(Name ="timed_out")]
		public bool TimedOut { get; internal set; }

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="updated")]
		public long Updated { get; internal set; }

		[DataMember(Name ="version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}
}
