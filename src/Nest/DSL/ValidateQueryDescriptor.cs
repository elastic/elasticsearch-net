using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Shared.Extensions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("IndicesValidateQuery")]
	public partial class ValidateQueryDescriptor<T> 
		:	QueryPathDescriptorBase<ValidateQueryDescriptor<T>, T, ValidateQueryRequestParameters>
		, IPathInfo<ValidateQueryRequestParameters> 
		where T : class
	{
		[JsonProperty("query")]
		public QueryContainer _Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			this._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		ElasticsearchPathInfo<ValidateQueryRequestParameters> IPathInfo<ValidateQueryRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.RequestParameters = this._QueryString;
			var source = this._QueryString.GetQueryStringValue<string>("source");
			var q = this._QueryString.GetQueryStringValue<string>("q");
			pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
