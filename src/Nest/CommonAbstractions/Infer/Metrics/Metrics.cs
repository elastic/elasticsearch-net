// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elasticsearch.Net;

namespace Nest
{
	public class Metrics : IEquatable<Metrics>, IUrlParameter
	{
		private IndicesStatsMetric? _indicesStat;
		private NodesStatsMetric? _nodesStats;
		private NodesInfoMetric? _nodesInfo;
		private ClusterStateMetric? _clusterStateMetric;
		private WatcherStatsMetric? _watcherStatsMetric;
		private NodesUsageMetric? _nodesUsageMetric;

		internal Metrics(IndicesStatsMetric metric) => (Value, _indicesStat) = (metric, metric);

		internal Metrics(NodesStatsMetric metric) => (Value, _nodesStats) = (metric, metric);

		internal Metrics(NodesInfoMetric metric) => (Value, _nodesInfo) = (metric, metric);

		internal Metrics(ClusterStateMetric metric) => (Value, _clusterStateMetric) = (metric, metric);

		internal Metrics(WatcherStatsMetric metric) => (Value, _watcherStatsMetric) = (metric, metric);

		internal Metrics(NodesUsageMetric metric) => (Value, _nodesUsageMetric) = (metric, metric);

		internal Enum Value { get; }

		public bool Equals(Metrics other) => Value.Equals(other?.Value);

		public string GetString(ITransportConfigurationValues settings)
		{
			if (_indicesStat != null) return _indicesStat.Value.GetStringValue();
			else if (_nodesStats != null) return _nodesStats.Value.GetStringValue();
			else if (_nodesInfo != null) return _nodesInfo.Value.GetStringValue();
			else if (_clusterStateMetric != null) return _clusterStateMetric.Value.GetStringValue();
			else if (_watcherStatsMetric != null) return _watcherStatsMetric.Value.GetStringValue();
			else if (_nodesUsageMetric != null) return _nodesUsageMetric.Value.GetStringValue();
			return Value.GetStringValue();
		}

		public static implicit operator Metrics(IndicesStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesInfoMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(ClusterStateMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(WatcherStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesUsageMetric metric) => new Metrics(metric);

		public bool Equals(Enum other) => Value.Equals(other);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is Metrics m && Equals(m.Value);

		public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

		public static bool operator ==(Metrics left, Metrics right) => Equals(left, right);

		public static bool operator !=(Metrics left, Metrics right) => !Equals(left, right);
	}
}
