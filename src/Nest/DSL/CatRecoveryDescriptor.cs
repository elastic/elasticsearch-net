using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatRecoveryRequest : IRequest<CatRecoveryRequestParameters>
	{
	}

	public partial class CatRecoveryRequest : BasePathRequest<CatRecoveryRequestParameters>, ICatRecoveryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatRecoveryDescriptor : BasePathDescriptor<CatRecoveryDescriptor, CatRecoveryRequestParameters>, ICatRecoveryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
