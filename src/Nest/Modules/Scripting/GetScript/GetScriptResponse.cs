using Newtonsoft.Json;

namespace Nest
{
	public interface IGetScriptResponse : IResponse
	{
		IStoredScript Script { get; }
	}

	public class GetScriptResponse : ResponseBase, IGetScriptResponse
	{
		[JsonProperty("script")]
		public IStoredScript Script { get; set; }
	}
}
