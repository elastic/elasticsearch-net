using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[DescriptorFor("IndicesValidateQuery")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ValidateQueryDescriptor<T> : QueryPathDescriptorBase<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, T>
		where T : class
	{
		[JsonProperty("query")]
		public QueryContainer _Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			this._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
		{
			var source = this.Request.RequestParameters.GetQueryStringValue<string>("source");
			var q = this.Request.RequestParameters.GetQueryStringValue<string>("q");
			pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
		}
	}
}
