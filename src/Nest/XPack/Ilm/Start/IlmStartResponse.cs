using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmStartResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmStartResponse : ResponseBase, IIlmStartResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
