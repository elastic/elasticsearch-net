using Elasticsearch.Net;
using Nest.Domain;
using Nest.Resolvers;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[DescriptorFor("IndicesStats")]
	public partial class IndicesStatsDescriptor : 
		IndicesOptionalPathDescriptor<IndicesStatsDescriptor, IndicesStatsRequestParameters>
		, IPathInfo<IndicesStatsRequestParameters>
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

		ElasticsearchPathInfo<IndicesStatsRequestParameters> IPathInfo<IndicesStatsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			if (this._Types.HasAny())
			{
				var inferrer = new ElasticInferrer(settings);
				var types = inferrer.TypeNames(this._Types);
				this._QueryString.AddQueryString("types", string.Join(",", types));
			}
			if (this._Metrics != null)
				pathInfo.Metric = this._Metrics.Cast<Enum>().GetStringValue();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
