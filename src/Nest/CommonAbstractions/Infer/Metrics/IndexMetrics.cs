using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public class IndexMetrics : IUrlParameter
	{
		private readonly NodesStatsIndexMetric _enumValue;

		public string GetString(IConnectionConfigurationValues settings) => this._enumValue.GetStringValue();
		internal IndexMetrics(NodesStatsIndexMetric metric) { _enumValue = metric; }

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);
	}
}
