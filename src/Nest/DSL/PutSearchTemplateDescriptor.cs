using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutSearchTemplateRequest : INamePath<SearchTemplateRequestParameters>
	{
	}

	public partial class PutSearchTemplateRequest : NamePathBase<SearchTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		public PutSearchTemplateRequest(string templateName)
			: base(templateName)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo);
		}
	}

	internal static class PutSearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}

	[DescriptorFor("SearchTemplatePut")]
	public partial class PutSearchTemplateDescriptor
		: NamePathDescriptor<PutSearchTemplateDescriptor, SearchTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo);
		}
	}
}
