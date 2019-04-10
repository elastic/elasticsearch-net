using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmMoveToStepResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }
	}

	public class IlmMoveToStepResponse : ResponseBase, IIlmMoveToStepResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
