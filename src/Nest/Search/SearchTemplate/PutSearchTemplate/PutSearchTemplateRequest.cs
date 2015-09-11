using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutSearchTemplateRequest : INamePath<PutTemplateRequestParameters>
	{
		[JsonProperty("template")]
		string Template { get; set; }
	}

	public partial class PutSearchTemplateRequest : NamePathBase<PutTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		public string Template { get; set; }

		public PutSearchTemplateRequest(string templateName)
			: base(templateName)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<PutTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}

	internal static class PutSearchTemplatePathInfo
	{
		public static void Update(RequestPath<PutTemplateRequestParameters> pathInfo, IPutSearchTemplateRequest request)
		{
			pathInfo.Id = request.Name;
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}

	[DescriptorFor("SearchTemplatePut")]
	public partial class PutSearchTemplateDescriptor
		: NamePathDescriptor<PutSearchTemplateDescriptor, PutTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		IPutSearchTemplateRequest Self => this;
		string IPutSearchTemplateRequest.Template { get; set;}

		public PutSearchTemplateDescriptor Template(string template)
		{
			this.Self.Template = template;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<PutTemplateRequestParameters> pathInfo)
		{
			PutSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
