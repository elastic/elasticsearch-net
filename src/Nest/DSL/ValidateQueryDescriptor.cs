using System;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("IndicesValidateQuery")]
	[JsonConverter(typeof(ActAsQueryConverter))]
	public partial class ValidateQueryDescriptor<T> 
		:	QueryPathDescriptorBase<ValidateQueryDescriptor<T>, T, ValidateQueryQueryString>
		, IActAsQuery
		, IPathInfo<ValidateQueryQueryString> 
		where T : class
	{
		BaseQuery IActAsQuery._Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			((IActAsQuery)this)._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		ElasticSearchPathInfo<ValidateQueryQueryString> IPathInfo<ValidateQueryQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ValidateQueryQueryString>(settings);
			pathInfo.QueryString = this._QueryString;
			var qs = this._QueryString;
			pathInfo.HttpMethod = (!qs._source.IsNullOrEmpty() || !qs._q.IsNullOrEmpty())
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
