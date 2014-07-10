using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IValidateQueryRequest : IQueryPath<ValidateQueryRequestParameters>
    {
        [JsonProperty("query")]
        IQueryContainer Query { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IValidateQueryRequest<T> : IValidateQueryRequest
        where T : class { }

    internal static class ValidateQueryPathInfo
    {
        public static void Update(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo, IValidateQueryRequest request)
        {
            var source = request.RequestParameters.GetQueryStringValue<string>("source");
            var q = request.RequestParameters.GetQueryStringValue<string>("q");
            pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
                ? PathInfoHttpMethod.GET
                : PathInfoHttpMethod.POST;
        }
    }

    public partial class ValidateQueryRequest : QueryPathBase<ValidateQueryRequestParameters>, IValidateQueryRequest
    {
        public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
        {
            ValidateQueryPathInfo.Update(pathInfo, this);
        }
    }

    public partial class ValidateQueryRequest<T> : QueryPathBase<ValidateQueryRequestParameters, T>, IValidateQueryRequest<T>
        where T : class
    {
        public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
        {
            ValidateQueryPathInfo.Update(pathInfo, this);
        }
    }

	[DescriptorFor("IndicesValidateQuery")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ValidateQueryDescriptor<T> 
        : QueryPathDescriptorBase<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, T>, IValidateQueryRequest<T>
		where T : class
	{
        private IValidateQueryRequest Self { get { return this; } }

        IQueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
		{
            ValidateQueryPathInfo.Update(pathInfo, this);
		}
	}
}
