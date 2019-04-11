using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmDeleteLifecycleResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmDeleteLifecycleResponse : ResponseBase, IIlmDeleteLifecycleResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
