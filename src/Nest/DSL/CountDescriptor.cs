using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> 
		:	QueryPathDescriptorBase<CountDescriptor<T>, T, CountRequestParameters>
		, IPathInfo<CountRequestParameters> 
		where T : class
	{
		[JsonProperty("query")]
		internal IQueryDescriptor _Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			this._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		ElasticsearchPathInfo<CountRequestParameters> IPathInfo<CountRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			var source = this._QueryString.GetQueryStringValue<string>("source");
			pathInfo.HttpMethod = !source.IsNullOrEmpty() 
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
