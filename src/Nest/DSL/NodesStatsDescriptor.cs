using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("NodesStats")]
	public partial class NodesStatsDescriptor : NodeIdOptionalDescriptor<NodesStatsDescriptor, NodesStatsQueryString>
		, IPathInfo<NodesStatsQueryString>
	{
		private IEnumerable<NodesStatsMetric> _Metrics { get; set; }
		private IEnumerable<NodesStatsIndexMetric> _IndexMetrics { get; set; }
		
		public NodesStatsDescriptor Metrics(params NodesStatsMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}
		public NodesStatsDescriptor IndexMetrics(params NodesStatsIndexMetric[] metrics)
		{
			this._IndexMetrics = metrics;
			return this;
		}

		ElasticsearchPathInfo<NodesStatsQueryString> IPathInfo<NodesStatsQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<NodesStatsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			if (this._IndexMetrics != null)
				pathInfo.IndexMetric = this._IndexMetrics.Cast<Enum>().GetStringValue();
			return pathInfo;
		}

	}
}
