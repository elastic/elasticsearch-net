// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Nest.Utf8Json;

namespace Nest
{
	public class CcrStatsResponse : ResponseBase
	{
		/// <inheritdoc cref="CcrStatsResponse.AutoFollowStats"/>
		[DataMember(Name ="auto_follow_stats")]
		public CcrAutoFollowStats AutoFollowStats { get; internal set; }

		/// <inheritdoc cref="CcrStatsResponse.FollowStats"/>
		[DataMember(Name = "follow_stats")]
		public CcrFollowStats FollowStats { get; internal set; }
	}

	public class CcrFollowStats
	{
		/// <inheritdoc cref="FollowIndexStatsResponse.Indices" />
		[DataMember(Name = "indices")]
		public IReadOnlyCollection<FollowIndexStats> Indices { get; internal set; } = EmptyReadOnly<FollowIndexStats>.Collection;
	}

	public class CcrAutoFollowStats
	{
		/// <summary>
		/// The number of indices that the auto-follow coordinator failed to automatically follow; the causes of recent failures are
		/// captured in the logs of the elected master node, and in the <see cref="RecentAutoFollowErrors"/> field.
		/// </summary>
		[DataMember(Name = "number_of_failed_follow_indices")]
		public long NumberOfFailedFollowIndices { get; internal set; }

		/// <summary>
		/// The number of times that the auto-follow coordinator failed to retrieve the cluster state from a
		/// remote cluster registered in a collection of auto-follow patterns.
		/// </summary>
		[DataMember(Name = "number_of_failed_remote_cluster_state_requests")]
		public long NumberOfFailedRemoteClusterStateRequests { get; internal set; }

		/// <summary>The number of indices that the auto-follow coordinator successfully followed.</summary>
		[DataMember(Name = "number_of_successful_follow_indices")]
		public long NumberOfSuccessfulFollowIndices { get; internal set; }

		/// <summary>An array of objects representing failures by the auto-follow coordinator.</summary>
		[DataMember(Name = "recent_auto_follow_errors")]
		public IReadOnlyCollection<ErrorCause> RecentAutoFollowErrors { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

		/// <summary>
		///  An array of auto followed clusters.
		/// </summary>
		[DataMember(Name = "auto_followed_clusters")]
		public IReadOnlyCollection< AutoFollowedCluster> AutoFollowedClusters { get; internal set; } = EmptyReadOnly<AutoFollowedCluster>.Collection;

	}

	public class AutoFollowedCluster
	{
		/// <summary>
		/// The cluster name.
		/// </summary>
		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		/// <summary>
		/// Time since last check.
		/// </summary>
		[DataMember(Name = "time_since_last_check_millis")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset TimeSinceLastCheck { get; internal set; }

		/// <summary>
		/// Last seen metadata version.
		/// </summary>
		[DataMember(Name = "last_seen_metadata_version")]
		public long LastSeenMetadataVersion { get; internal set; }
	}
}
