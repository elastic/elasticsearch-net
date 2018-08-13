using Newtonsoft.Json;

namespace Nest
{
	public interface IExecutePainlessScriptResponse : IResponse
	{
		[JsonProperty("result")]
		string Result { get; }
	}

	public class ExecutePainlessScriptResponse : ResponseBase, IExecutePainlessScriptResponse
	{
		public string Result { get; set; }
	}
}
