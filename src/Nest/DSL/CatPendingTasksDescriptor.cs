using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatPendingTasksRequest : IRequest<CatPendingTasksRequestParameters>
	{
	}

	public partial class CatPendingTasksRequest : BasePathRequest<CatPendingTasksRequestParameters>, ICatPendingTasksRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatPendingTasksDescriptor : BasePathDescriptor<CatPendingTasksDescriptor, CatPendingTasksRequestParameters>, ICatPendingTasksRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
