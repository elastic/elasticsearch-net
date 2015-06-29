using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IExplainRequest : IDocumentOptionalPath<ExplainRequestParameters>
    {
        [JsonProperty("query")]
        IQueryContainer Query { get; set; }
    }
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IExplainRequest<T> : IExplainRequest
        where T : class { }

    internal static class ExplainPathInfo
    {
        public static void Update(ElasticsearchPathInfo<ExplainRequestParameters> pathInfo, IExplainRequest request)
        {
            var source = request.RequestParameters.GetQueryStringValue<string>("source");
            var q = request.RequestParameters.GetQueryStringValue<string>("q");
            pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
                ? PathInfoHttpMethod.GET
                : PathInfoHttpMethod.POST;
        }
    }

    public partial class ExplainRequest : DocumentPathBase<ExplainRequestParameters>, IExplainRequest
    {
	    public ExplainRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

	    public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ExplainRequestParameters> pathInfo)
        {
            ExplainPathInfo.Update(pathInfo, this);
        }
    }

	public partial class ExplainRequest<T> : DocumentPathBase<ExplainRequestParameters, T>, IExplainRequest<T>
        where T : class
    {
	    public ExplainRequest(string id) : base(id) { } 
	    public ExplainRequest(long id) : base(id) { }
	    public ExplainRequest(T document) : base(document) { }

	    public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ExplainRequestParameters> pathInfo)
        {
            ExplainPathInfo.Update(pathInfo, this);
        }
    }

	[DescriptorFor("Explain")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ExplainDescriptor<T> : DocumentPathDescriptor<ExplainDescriptor<T>, ExplainRequestParameters, T>, IExplainRequest<T>
		where T : class
	{
        private IExplainRequest Self => this;

        IQueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ExplainRequestParameters> pathInfo)
		{
            ExplainPathInfo.Update(pathInfo, this);
		}
	}
}
