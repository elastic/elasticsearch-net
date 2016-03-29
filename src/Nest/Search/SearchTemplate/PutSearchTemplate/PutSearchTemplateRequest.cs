using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutSearchTemplateRequest>))]
	public partial interface IPutSearchTemplateRequest 
	{
		[JsonProperty("template")]
		string Template { get; set; }
	}

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
