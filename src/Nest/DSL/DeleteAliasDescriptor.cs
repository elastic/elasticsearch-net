using Elasticsearch.Net;

namespace Nest
{
	public interface IDeleteAliasRequest : IIndexNamePath<DeleteAliasRequestParameters>
	{
	}

	internal static class DeleteAliasPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteAliasRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}

	public partial class DeleteAliasRequest : IndexNamePathBase<DeleteAliasRequestParameters>, IDeleteAliasRequest
	{
		public DeleteAliasRequest(string index, string name) : base(index, name) { }
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteAliasRequestParameters> pathInfo)
		{
			DeleteAliasPathInfo.Update(pathInfo);
		}
	}

	[DescriptorFor("IndicesDeleteAlias")]
	public partial class DeleteAliasDescriptor<T> 
		: IndexNamePathDescriptor<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, T>, IDeleteAliasRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteAliasRequestParameters> pathInfo)
		{
			DeleteAliasPathInfo.Update(pathInfo);
		}
	}
}
