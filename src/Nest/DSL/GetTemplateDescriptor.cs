using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetTemplateRequest : INamePath<GetTemplateRequestParameters> { }

	internal static class GetTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo, IGetTemplateRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetTemplateRequest : NamePathBase<GetTemplateRequestParameters>, IGetTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo)
		{
			GetTemplatePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor : NamePathDescriptor<GetTemplateDescriptor, GetTemplateRequestParameters>, IGetTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo)
		{
			GetTemplatePathInfo.Update(pathInfo, this);
		}
		
	}
}
