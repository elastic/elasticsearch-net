using Newtonsoft.Json;
using System;

namespace Nest
{
	[Obsolete("Removed in NEST 6.x. In NEST 6.x, use the PutScript API to store templates")]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutSearchTemplateRequest>))]
	public partial interface IPutSearchTemplateRequest
	{
		[JsonProperty("template")]
		string Template { get; set; }
	}

	[Obsolete("Removed in NEST 6.x. In NEST 6.x, use the PutScript API to store templates")]
	public partial class PutSearchTemplateRequest
	{
		public string Template { get; set; }
	}

	[Obsolete("Removed in NEST 6.x. In NEST 6.x, use the PutScript API to store templates")]
	[DescriptorFor("PutTemplate")]
	public partial class PutSearchTemplateDescriptor
	{
		string IPutSearchTemplateRequest.Template { get; set; }

		public PutSearchTemplateDescriptor Template(string template) => Assign(a => a.Template = template);
	}
}
