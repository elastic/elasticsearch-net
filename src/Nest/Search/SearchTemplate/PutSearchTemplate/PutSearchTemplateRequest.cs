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
		public string Template { get; set; }
	}

	[DescriptorFor("PutTemplate")]
	public partial class PutSearchTemplateDescriptor 
	{
		string IPutSearchTemplateRequest.Template { get; set; }

		public PutSearchTemplateDescriptor Template(string template) => Assign(a => a.Template = template);
	}
}
