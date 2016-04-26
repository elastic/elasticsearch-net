using Newtonsoft.Json;

namespace Nest
{
	public interface IGetPipelineResponse : IResponse
	{
		string Pipeline { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetPipelineResponse : ResponseBase, IGetPipelineResponse
	{
		[JsonProperty("script")]
		public string Pipeline { get; set; }
	}
}
