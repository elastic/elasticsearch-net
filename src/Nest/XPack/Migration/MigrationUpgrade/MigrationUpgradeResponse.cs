using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMigrationUpgradeResponse : IResponse
	{
		[DataMember(Name ="batches")]
		long Batches { get; }

		[DataMember(Name ="created")]
		long Created { get; }

		[DataMember(Name ="deleted")]
		long Deleted { get; }

		[DataMember(Name ="failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[DataMember(Name ="noops")]
		long Noops { get; }

		[DataMember(Name ="retries")]
		Retries Retries { get; }

		/// <summary>
		/// The id of the task if <see cref="MigrationUpgradeRequestParameters.WaitForCompletion" />
		/// is set to <c>false</c> on the request
		/// </summary>
		[DataMember(Name ="task")]
		TaskId Task { get; }

		[DataMember(Name ="throttled_millis")]
		long ThrottledMilliseconds { get; }

		[DataMember(Name ="timed_out")]
		bool TimedOut { get; }

		[DataMember(Name ="took")]
		long Took { get; }

		[DataMember(Name ="total")]
		long Total { get; }

		[DataMember(Name ="updated")]
		long Updated { get; }

		[DataMember(Name ="version_conflicts")]
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
