using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface ICcrStatsResponse : IResponse
	{
		[DataMember(Name ="auto_follow_stats")]
		CcrAutoFollowStats AutoFollowStats { get; }

		[DataMember(Name = "follow_stats")]
		CcrFollowStats FollowStats { get; }
	}

	public class CcrStatsResponse : ResponseBase, ICcrStatsResponse
	{
		/// <inheritdoc cref="ICcrStatsResponse.AutoFollowStats"/>
		public CcrAutoFollowStats AutoFollowStats { get; internal set; }

		/// <inheritdoc cref="ICcrStatsResponse.FollowStats"/>
		public CcrFollowStats FollowStats { get; internal set; }
	}


	public class CcrFollowStats
	{
		/// <inheritdoc cref="IFollowIndexStatsResponse.Indices" />
		[DataMember(Name = "indices")]
		public IReadOnlyCollection<FollowIndexStats> Indices { get; internal set; } = EmptyReadOnly<FollowIndexStats>.Collection;
	}

	public class CcrAutoFollowStats
	{

		/// <summary>
		/// the number of indices that the auto-follow coordinator failed to automatically follow; the causes of recent failures are
		/// captured in the logs of the elected master node, and in the auto_follow_stats.recent_auto_follow_errors field
		/// </summary>
		[DataMember(Name = "number_of_failed_follow_indices")]
		public long NumberOfFailedFollowIndices { get; internal set; }

		/// <summary>
		/// the number of times that the auto-follow coordinator failed to retrieve the cluster state from a
		/// remote cluster registered in a collection of auto-follow patterns
		/// </summary>
		[DataMember(Name = "number_of_failed_remote_cluster_state_requests")]
		public long NumberOfFailedRemoteClusterStateRequests { get; internal set; }

		/// <summary> the number of indices that the auto-follow coordinator successfully followed </summary>
		[DataMember(Name = "number_of_successful_follow_indices")]
		public long NumberOfSuccessfulFollowIndices { get; internal set; }

		/// <summary> an array of objects representing failures by the auto-follow coordinator </summary>
		[DataMember(Name = "recent_auto_follow_errors")]
		public IReadOnlyCollection<ErrorCause> RecentAutoFollowErrors { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

	}
}
