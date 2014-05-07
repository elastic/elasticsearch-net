using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor : NodeIdOptionalDescriptor<NodesInfoDescriptor, NodesInfoRequestParameters>
		, IPathInfo<NodesInfoRequestParameters>
	{
		private IEnumerable<NodesInfoMetric> _Metrics { get; set; }
		public NodesInfoDescriptor Metrics(params NodesInfoMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}
		ElasticsearchPathInfo<NodesInfoRequestParameters> IPathInfo<NodesInfoRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			return pathInfo;
		}

	}
}
