using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IGetScriptResponse : IResponse
	{
		string Script { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetScriptResponse : ResponseBase, IGetScriptResponse
	{
		[JsonProperty("script")]
		public string Script { get; set; }
	}
}