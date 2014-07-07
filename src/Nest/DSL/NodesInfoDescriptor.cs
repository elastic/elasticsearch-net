using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor : NodeIdOptionalDescriptor<NodesInfoDescriptor, NodesInfoRequestParameters>
	{
		private IEnumerable<NodesInfoMetric> _Metrics { get; set; }
		public NodesInfoDescriptor Metrics(params NodesInfoMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo)
		{
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}

	}
}
