using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSearchTemplateRequest : INamePath<GetTemplateRequestParameters>
	{
	}

	public partial class GetSearchTemplateRequest 
		: NamePathBase<GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		public GetSearchTemplateRequest(string templateName)
			: base(templateName)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}

	internal static class GetSearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo, IGetSearchTemplateRequest request)
		{
			pathInfo.Id = request.Name;
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}

	public partial class GetSearchTemplateDescriptor 
		: NamePathDescriptor<GetSearchTemplateDescriptor, GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
