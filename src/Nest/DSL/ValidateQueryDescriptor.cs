using System;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("IndicesValidateQuery")]
	public partial class ValidateQueryDescriptor<T> 
		:	QueryPathDescriptorBase<ValidateQueryDescriptor<T>, T, ValidateQueryQueryString>
		, IPathInfo<ValidateQueryQueryString> 
		where T : class
	{
		[JsonProperty("query")]
		public BaseQuery _Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			this._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		ElasticsearchPathInfo<ValidateQueryQueryString> IPathInfo<ValidateQueryQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ValidateQueryQueryString>(settings, this._QueryString);
			pathInfo.QueryString = this._QueryString;
			var qs = this._QueryString;
			pathInfo.HttpMethod = (!qs._source.IsNullOrEmpty() || !qs._q.IsNullOrEmpty())
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
