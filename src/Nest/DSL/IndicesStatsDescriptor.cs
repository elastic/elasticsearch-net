using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("IndicesStats")]
	public partial class IndicesStatsDescriptor : IndicesOptionalPathDescriptor<IndicesStatsDescriptor, IndicesStatsRequestParameters>
	{
		private IEnumerable<TypeNameMarker> _Types { get; set; }
		private IEnumerable<IndicesStatsMetric> _Metrics { get; set; }
		
		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesStatsDescriptor Types(params Type[] completion_fields)
		{
			this._Types = completion_fields.Select(t=>(TypeNameMarker)t);
			return this;
		}

		public IndicesStatsDescriptor Metrics(params IndicesStatsMetric[] metrics)
		{
			this._Metrics = metrics;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndicesStatsRequestParameters> pathInfo)
		{
			if (this._Types.HasAny())
			{
				var inferrer = new ElasticInferrer(settings);
				var types = inferrer.TypeNames(this._Types);
				this.Request.RequestParameters.AddQueryString("types", string.Join(",", types));
			}
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
