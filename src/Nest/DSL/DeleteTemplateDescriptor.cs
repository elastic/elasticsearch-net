using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteTemplateRequest : INamePath<DeleteTemplateRequestParameters> { }

	internal static class DeleteTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo, IDeleteTemplateRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteTemplateRequest : NamePathBase<DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
		public DeleteTemplateRequest(string name) : base(name)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteTemplatePathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor : NamePathDescriptor<DeleteTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteTemplatePathInfo.Update(pathInfo, this);
		}

	}
}
