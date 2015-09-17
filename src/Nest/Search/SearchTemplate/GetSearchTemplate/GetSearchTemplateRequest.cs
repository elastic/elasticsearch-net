using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSearchTemplateRequest : IRequest<GetTemplateRequestParameters>
	{
		//TODO NAME
	}

	internal static class GetSearchTemplatePathInfo
	{
		public static void Update(RequestPath<GetTemplateRequestParameters> pathInfo, IGetSearchTemplateRequest request)
		{
			//TODO NAME pathInfo.Id = request.;
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}

	public partial class GetSearchTemplateRequest : RequestBase<GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<GetTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}


	public partial class GetSearchTemplateDescriptor : RequestDescriptorBase<GetSearchTemplateDescriptor, GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<GetTemplateRequestParameters> pathInfo)
		{
			GetSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
