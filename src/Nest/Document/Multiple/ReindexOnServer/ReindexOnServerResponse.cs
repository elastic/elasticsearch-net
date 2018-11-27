using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IReindexOnServerResponse : IResponse
	{
		[DataMember(Name ="batches")]
		long Batches { get; }

		[DataMember(Name ="created")]
		long Created { get; }

		[DataMember(Name ="failures")]
		IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }

		[DataMember(Name ="noops")]
		long Noops { get; }

		[DataMember(Name ="retries")]
		Retries Retries { get; }

		[DataMember(Name ="slice_id")]
		int? SliceId { get; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[DataMember(Name ="task")]
		TaskId Task { get; }

		[DataMember(Name ="timed_out")]
		bool TimedOut { get; }

		//https://github.com/elastic/elasticsearch/commit/11f90bffda50f0acc8dc1409f3f33005e1249234
		// 2.3 released this writing the time value's to string e.g 123.4ms instead of long as all the others took responses
		[DataMember(Name ="took")]
		Time Took { get; }

		[DataMember(Name ="total")]
		long Total { get; }

		[DataMember(Name ="updated")]
		long Updated { get; }

		[DataMember(Name ="version_conflicts")]
		long VersionConflicts { get; }
	}

	[DataContract]
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
		/// Only has a value if WaitForCompletion is set to <c>false</c> on the request
		/// </summary>
		public TaskId Task { get; internal set; }

		public bool TimedOut { get; internal set; }

		public Time Took { get; internal set; }

		public long Total { get; internal set; }

		public long Updated { get; internal set; }

		public long VersionConflicts { get; internal set; }
	}
}
