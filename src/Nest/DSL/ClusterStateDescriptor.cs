using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class ClusterStateDescriptor : 
		IndicesOptionalPathDescriptor<ClusterStateDescriptor, ClusterStateQueryString>
		, IPathInfo<ClusterStateQueryString>
	{
		
		private IEnumerable<ClusterStateMetric> _Metrics { get; set; }
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}
		ElasticsearchPathInfo<ClusterStateQueryString> IPathInfo<ClusterStateQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<ClusterStateQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			return pathInfo;
		}
	}
}
