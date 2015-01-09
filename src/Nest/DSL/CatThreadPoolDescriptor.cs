using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatThreadPoolRequest : IRequest<CatThreadPoolRequestParameters>
	{
	}

	public partial class CatThreadPoolRequest : BasePathRequest<CatThreadPoolRequestParameters>, ICatThreadPoolRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatThreadPoolDescriptor : BasePathDescriptor<CatThreadPoolDescriptor, CatThreadPoolRequestParameters>, ICatThreadPoolRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
