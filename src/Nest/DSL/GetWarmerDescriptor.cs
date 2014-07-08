using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetWarmerRequest : IIndicesOptionalTypesNamePath<GetWarmerRequestParameters> { }

	internal static class GetWarmerPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo, IGetWarmerRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetWarmerRequest : IndicesOptionalTypesNamePathBase<GetWarmerRequestParameters>, IGetWarmerRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo)
		{
			GetWarmerPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : IndicesOptionalTypesNamePathDescriptor<GetWarmerDescriptor, GetWarmerRequestParameters>, IGetWarmerRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
