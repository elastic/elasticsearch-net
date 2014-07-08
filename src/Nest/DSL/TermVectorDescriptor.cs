using Elasticsearch.Net;

namespace Nest
{
    public interface ITermvectorRequest : IDocumentOptionalPath<TermvectorRequestParameters> { }

    public interface ITermvectorRequest<T> : ITermvectorRequest where T : class { }

    internal static class TermvectorPathInfo
    {
        public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            pathInfo.HttpMethod = PathInfoHttpMethod.GET;
        }
    }

    public partial class TermvectorRequest : DocumentPathBase<TermvectorRequestParameters>, ITermvectorRequest
    {
	    public TermvectorRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

	    protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            TermvectorPathInfo.Update(settings, pathInfo);
        }
    }

    public partial class TermvectorRequest<T> : DocumentPathBase<TermvectorRequestParameters, T>, ITermvectorRequest<T>
        where T : class
    {
	    public TermvectorRequest(string id) : base(id) { }

	    public TermvectorRequest(long id) : base(id) { }

	    public TermvectorRequest(T document) : base(document) { }

	    protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            TermvectorPathInfo.Update(settings, pathInfo);
        }
    }

	public partial class TermvectorDescriptor<T> : DocumentPathDescriptor<TermvectorDescriptor<T>, TermvectorRequestParameters, T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
            TermvectorPathInfo.Update(settings, pathInfo);
		}
	}
}
