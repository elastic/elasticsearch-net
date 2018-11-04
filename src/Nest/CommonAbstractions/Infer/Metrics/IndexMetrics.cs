using Elasticsearch.Net;

namespace Nest
{
	public class IndexMetrics : IUrlParameter
	{
		private readonly NodesStatsIndexMetric _enumValue;

		internal IndexMetrics(NodesStatsIndexMetric metric) => _enumValue = metric;

		public string GetString(IConnectionConfigurationValues settings) => _enumValue.GetStringValue();

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);
	}
}
