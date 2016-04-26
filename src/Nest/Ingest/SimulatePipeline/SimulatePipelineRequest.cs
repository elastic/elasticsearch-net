using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISimulatePipelineRequest
	{
		[JsonProperty("script")]
		string Pipeline { get; set; }
	}

	public partial class SimulatePipelineRequest
	{
		public string Pipeline { get; set; }
	}

	[DescriptorFor("IngestSimulate")]
	public partial class SimulatePipelineDescriptor
	{
		string ISimulatePipelineRequest.Pipeline { get; set; }

		public SimulatePipelineDescriptor Pipeline(string script) => Assign(a => a.Pipeline = script);
	}
}
