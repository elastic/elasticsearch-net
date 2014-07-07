using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ClusterStateDescriptor : IndicesOptionalPathDescriptor<ClusterStateDescriptor, ClusterStateRequestParameters>
	{
		
		private IEnumerable<ClusterStateMetric> _Metrics { get; set; }
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterStateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
		}
	}
}
