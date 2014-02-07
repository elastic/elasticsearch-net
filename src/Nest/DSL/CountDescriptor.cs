using System;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("Count")]
	[JsonConverter(typeof(ActAsQueryConverter))]
	public partial class CountDescriptor<T> 
		:	QueryPathDescriptorBase<CountDescriptor<T>, T, CountQueryString>
		, IActAsQuery
		, IPathInfo<CountQueryString> 
		where T : class
	{
		BaseQuery IActAsQuery._Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			((IActAsQuery)this)._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		ElasticsearchPathInfo<CountQueryString> IPathInfo<CountQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<CountQueryString>(settings, this._QueryString);
			var qs = this._QueryString;
			pathInfo.HttpMethod = !qs._source.IsNullOrEmpty() 
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
