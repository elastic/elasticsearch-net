using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexMetrics : IEquatable<IndexMetrics>, IUrlParameter
	{
		private readonly NodesStatsIndexMetric _enumValue;
		internal Enum Value => _enumValue;

		public string GetString(IConnectionConfigurationValues settings) => this._enumValue.GetStringValue();
		internal IndexMetrics(NodesStatsIndexMetric metric) { _enumValue = metric; }

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);

		public bool Equals(Enum other) => this.Value.Equals(other);
		public bool Equals(IndexMetrics other) => this.Value.Equals(other.Value);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is IndexMetrics m && Equals(m.Value);

		public override int GetHashCode() => (_enumValue.GetHashCode());

		public static bool operator ==(IndexMetrics left, IndexMetrics right) => Equals(left, right);

		public static bool operator !=(IndexMetrics left, IndexMetrics right) => !Equals(left, right);
	}
}
