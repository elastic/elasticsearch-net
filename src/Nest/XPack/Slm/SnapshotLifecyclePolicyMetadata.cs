using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Metadata about a Snapshot lifecycle policy
	/// </summary>
	public class SnapshotLifecyclePolicyMetadata
	{
		/// <summary>
		/// The modified date.
		/// Returned only when Human is set to <c>true</c> on the request
		/// </summary>
		[DataMember(Name = "modified_date")]
		public DateTimeOffset? ModifiedDate { get; internal set; }

		/// <summary>
		/// The modified date in milliseconds
		/// </summary>
		[DataMember(Name = "modified_date_millis")]
		public long ModifiedDateInMilliseconds { get; internal set; }

		/// <summary>
		/// The next execution date.
		/// Returned only when Human is set to <c>true</c> on the request
		/// </summary>
		[DataMember(Name = "next_execution")]
		public DateTimeOffset? NextExecution { get; internal set; }

		/// <summary>
		/// The next execution date in milliseconds
		/// </summary>
		[DataMember(Name = "next_execution_millis")]
		public long NextExecutionInMilliseconds { get; internal set; }

		/// <summary>
		/// The snapshot lifecycle policy
		/// </summary>
		[DataMember(Name = "policy")]
		public SnapshotLifecyclePolicy Policy { get; internal set; }

		/// <summary>
		/// The version
		/// </summary>
		[DataMember(Name = "version")]
		public int Version { get; internal set; }
	}
}
