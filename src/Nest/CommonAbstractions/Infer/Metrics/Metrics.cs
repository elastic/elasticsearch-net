using System;
using Elasticsearch.Net;

namespace Nest
{
	public class Metrics : IEquatable<Metrics>, IUrlParameter
	{
		private readonly Enum _enumValue;
		internal Enum Value => _enumValue;

		public string GetString(IConnectionConfigurationValues settings) => this._enumValue.GetStringValue();
		internal Metrics(IndicesStatsMetric metric) { _enumValue = metric; }
		internal Metrics(NodesStatsMetric metric){ _enumValue = metric; }
		internal Metrics(NodesInfoMetric metric){ _enumValue = metric; }
		internal Metrics(ClusterStateMetric metric){ _enumValue = metric; }
		internal Metrics(WatcherStatsMetric metric){ _enumValue = metric; }
		internal Metrics(NodesUsageMetric metric){ _enumValue = metric; }

		public static implicit operator Metrics(IndicesStatsMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(NodesStatsMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(NodesInfoMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(ClusterStateMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(WatcherStatsMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(NodesUsageMetric metric) => new Metrics(metric);

		public bool Equals(Enum other) => this.Value.Equals(other);
		public bool Equals(Metrics other) => this.Value.Equals(other.Value);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is Metrics m && Equals(m.Value);

		public override int GetHashCode() => (_enumValue != null ? _enumValue.GetHashCode() : 0);

		public static bool operator ==(Metrics left, Metrics right) => Equals(left, right);

		public static bool operator !=(Metrics left, Metrics right) => !Equals(left, right);
	}
}
