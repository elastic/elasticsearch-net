using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatFielddataRequest : IRequest<CatFielddataRequestParameters>
	{
	}

	public partial class CatFielddataRequest : BasePathRequest<CatFielddataRequestParameters>, ICatFielddataRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatFielddataDescriptor : BasePathDescriptor<CatFielddataDescriptor, CatFielddataRequestParameters>, ICatFielddataRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
