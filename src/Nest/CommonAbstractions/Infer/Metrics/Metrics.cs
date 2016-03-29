using System;
using Elasticsearch.Net;

namespace Nest
{
	public class Metrics : IUrlParameter
	{
		private readonly Enum _enumValue;

		public string GetString(IConnectionConfigurationValues settings) => KnownEnums.Resolve(this._enumValue);
		internal Metrics(IndicesStatsMetric metric) { _enumValue = metric; }
		internal Metrics(NodesStatsMetric metric){ _enumValue = metric; }
		internal Metrics(NodesInfoMetric metric){ _enumValue = metric; }
		internal Metrics(ClusterStateMetric metric){ _enumValue = metric; }

		public static implicit operator Metrics(IndicesStatsMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(NodesStatsMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(NodesInfoMetric metric) => new Metrics(metric);
		public static implicit operator Metrics(ClusterStateMetric metric) => new Metrics(metric);
	}
}
