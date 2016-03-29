using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexMetrics : IUrlParameter
	{
		private readonly Enum _enumValue;

		public string GetString(IConnectionConfigurationValues settings) => KnownEnums.Resolve(this._enumValue);
		internal IndexMetrics(NodesStatsIndexMetric metric) { _enumValue = metric; }

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);
	}
}
