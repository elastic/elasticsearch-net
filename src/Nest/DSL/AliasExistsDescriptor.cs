using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasExistsRequest : IIndexOptionalNamePath<AliasExistsRequestParameters> { }

	internal static class AliasExistsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<AliasExistsRequestParameters> pathInfo, IAliasExistsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
	
	public partial class AliasExistsRequest : IndexOptionalNamePathBase<AliasExistsRequestParameters>, IAliasExistsRequest
	{
		public AliasExistsRequest(string name) : base(name) { }
		public AliasExistsRequest(IndexNameMarker index, string name) : base(index, name) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasExistsRequestParameters> pathInfo)
		{
			AliasExistsPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesExistsAlias")]
	public partial class AliasExistsDescriptor : IndexOptionalNamePathDescriptor<AliasExistsDescriptor, AliasExistsRequestParameters>, IAliasExistsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasExistsRequestParameters> pathInfo)
		{
			AliasExistsPathInfo.Update(pathInfo, this);
		}
	}
}
