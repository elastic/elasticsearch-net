using Newtonsoft.Json;

namespace Nest
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