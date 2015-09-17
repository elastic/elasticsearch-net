using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSearchTemplateRequest : IRequest<DeleteTemplateRequestParameters>
	{
		//TODO NAME/TEMPLATE
	}

	internal static class DeleteSearchTemplatePathInfo
	{
		public static void Update(RequestPath<DeleteTemplateRequestParameters> pathInfo, IDeleteSearchTemplateRequest request)
		{
			//TODO pathInfo.Id = request.Name;
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}
	
	public partial class DeleteSearchTemplateRequest : RequestBase<DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}


	public partial class DeleteSearchTemplateDescriptor 
		: RequestDescriptorBase<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
