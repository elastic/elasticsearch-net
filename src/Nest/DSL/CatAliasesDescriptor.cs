using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal static class CatRequestPathInfo
	{
		public static void Update(IElasticsearchPathInfo pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatAliasesRequest : IRequest<CatAliasesRequestParameters>
	{
	}

	public partial class CatAliasesRequest : BasePathRequest<CatAliasesRequestParameters>, ICatAliasesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatAliasesDescriptor : BasePathDescriptor<CatAliasesDescriptor, CatAliasesRequestParameters>, ICatAliasesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
