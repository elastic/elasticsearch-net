using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmRetryResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmRetryResponse : ResponseBase, IIlmRetryResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
