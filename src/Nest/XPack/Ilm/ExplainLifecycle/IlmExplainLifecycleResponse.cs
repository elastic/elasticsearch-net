using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmExplainLifecycleResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmExplainLifecycleResponse : ResponseBase, IIlmExplainLifecycleResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
