using Elasticsearch.Net;
using Newtonsoft.Json;
namespace Nest
{
	internal static class UpgradePathInfo
	{
		public static void Update(ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeRequest : IIndicesOptionalPath<UpgradeRequestParameters>
	{
	}

	public partial class UpgradeRequest : IndicesOptionalPathBase<UpgradeRequestParameters>, IUpgradeRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo)
		{
			UpgradePathInfo.Update(pathInfo);
		}
	}

	[DescriptorFor("IndicesUpgrade")]
	public partial class UpgradeDescriptor 
		: IndicesOptionalPathDescriptor<UpgradeDescriptor, UpgradeRequestParameters>, IUpgradeRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo)
		{
			UpgradePathInfo.Update(pathInfo);
		}
	}
}
