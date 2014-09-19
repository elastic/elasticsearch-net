using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSearchTemplateRequest : INamePath<SearchTemplateRequestParameters> { }

	public partial class DeleteSearchTemplateRequest 
		: NamePathBase<SearchTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		public DeleteSearchTemplateRequest(string templateName)
			: base(templateName)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo);
		}
	}

	internal static class DeleteSearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	[DescriptorFor("SearchTemplateDelete")]
	public partial class DeleteSearchTemplateDescriptor 
		: NamePathDescriptor<DeleteSearchTemplateDescriptor, SearchTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo);
		}
	}
}
