using Newtonsoft.Json;
using System;

namespace Nest
{
	[Obsolete("Removed in NEST 6.x.")]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutSearchTemplateRequest>))]
	public partial interface IPutSearchTemplateRequest
	{
		[JsonProperty("template")]
		string Template { get; set; }
	}

	[Obsolete("Removed in NEST 6.x.")]
	public partial class PutSearchTemplateRequest
	{
		public string Template { get; set; }
	}

	[Obsolete("Removed in NEST 6.x.")]
	[DescriptorFor("PutTemplate")]
	public partial class PutSearchTemplateDescriptor
	{
		string IPutSearchTemplateRequest.Template { get; set; }

		public PutSearchTemplateDescriptor Template(string template) => Assign(a => a.Template = template);
	}
}
