using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmPutLifecycleResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmPutLifecycleResponse : ResponseBase, IIlmPutLifecycleResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
