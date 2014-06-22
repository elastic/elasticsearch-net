using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System;

namespace Nest
{
	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> 
		:	QueryPathDescriptorBase<CountDescriptor<T>, T, CountRequestParameters>
		, IPathInfo<CountRequestParameters> 
		where T : class
	{
		[JsonProperty("query")]
		internal IQueryContainer _Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
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
