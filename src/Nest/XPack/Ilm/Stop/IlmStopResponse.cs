using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmStopResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmStopResponse : ResponseBase, IIlmStopResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
