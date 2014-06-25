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
