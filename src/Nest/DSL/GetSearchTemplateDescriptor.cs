using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSearchTemplateRequest : INamePath<SearchTemplateRequestParameters>
	{
	}

	public partial class GetSearchTemplateRequest 
		: NamePathBase<SearchTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		public GetSearchTemplateRequest(string templateName)
			: base(templateName)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo);
		}
	}

	internal static class GetSearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	[DescriptorFor("SearchTemplateGet")]
	public partial class GetSearchTemplateDescriptor 
		: NamePathDescriptor<GetSearchTemplateDescriptor, SearchTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo);
		}
	}
}
