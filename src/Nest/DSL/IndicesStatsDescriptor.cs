using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesStats")]
	public partial class IndicesStatsDescriptor : 
		IndicesOptionalPathDescriptor<IndicesStatsDescriptor, IndicesStatsQueryString>
		, IPathInfo<IndicesStatsQueryString>
	{
		private IEnumerable<TypeNameMarker> _Types { get; set; }
		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesStatsDescriptor Types(params string[] completion_fields)
		{
			this._Types = completion_fields.Select(t=>(TypeNameMarker)t);
			return this;
		}
		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesStatsDescriptor Types(params Type[] completion_fields)
		{
			this._Types = completion_fields.Select(t=>(TypeNameMarker)t);
			return this;
		}
		ElasticsearchPathInfo<IndicesStatsQueryString> IPathInfo<IndicesStatsQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<IndicesStatsQueryString>(settings, this._QueryString);
			if (this._Types.HasAny())
			{
				var inferrer = new ElasticInferrer(settings);
				var types = inferrer.TypeNames(this._Types);
				this._QueryString.Add("types", string.Join(",", types));
			}
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}
