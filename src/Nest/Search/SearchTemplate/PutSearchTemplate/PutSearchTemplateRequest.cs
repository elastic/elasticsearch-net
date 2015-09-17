using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutSearchTemplateRequest : IRequest<PutTemplateRequestParameters>
	{
		//TODO NAME
		[JsonProperty("template")]
		string Template { get; set; }
	}

	internal static class PutSearchTemplatePathInfo
	{
		public static void Update(RequestPath<PutTemplateRequestParameters> pathInfo, IPutSearchTemplateRequest request)
		{
			//TODO NAME pathInfo.Id = request.Name;
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}

	public partial class PutSearchTemplateRequest : RequestBase<PutTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		public string Template { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<PutTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SearchTemplatePut")]
	public partial class PutSearchTemplateDescriptor : RequestDescriptorBase<PutSearchTemplateDescriptor, PutTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		IPutSearchTemplateRequest Self => this;
		string IPutSearchTemplateRequest.Template { get; set;}

		public PutSearchTemplateDescriptor Template(string template)
		{
			this.Self.Template = template;
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<PutTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
