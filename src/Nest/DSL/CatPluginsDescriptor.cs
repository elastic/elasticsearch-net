using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatPluginsRequest : IRequest<CatPluginsRequestParameters>
	{
	}

	public partial class CatPluginsRequest : BasePathRequest<CatPluginsRequestParameters>, ICatPluginsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatPluginsDescriptor : BasePathDescriptor<CatPluginsDescriptor, CatPluginsRequestParameters>, ICatPluginsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
