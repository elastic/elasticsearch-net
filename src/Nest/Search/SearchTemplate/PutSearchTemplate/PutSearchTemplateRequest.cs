using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface IPutSearchTemplateRequest 
	{
		//TODO NAME
		[JsonProperty("template")]
		string Template { get; set; }
	}


	//TODO whats going on here? renamed?
	public partial class PutSearchTemplateRequest 
	{
		public PutSearchTemplateRequest(Id id) : base(r => r.Required(Ids.Single(id))) { }

		public string Template { get; set; }
	}

	[DescriptorFor("SearchTemplatePut")]
	public partial class PutSearchTemplateDescriptor : RequestDescriptorBase<PutSearchTemplateDescriptor, PutTemplateRequestParameters>, IPutSearchTemplateRequest
	{
		IPutSearchTemplateRequest Self => this;
		string IPutSearchTemplateRequest.Template { get; set; }

		public PutSearchTemplateDescriptor Template(string template)
		{
			this.Self.Template = template;
			return this;
		}
	}
}
