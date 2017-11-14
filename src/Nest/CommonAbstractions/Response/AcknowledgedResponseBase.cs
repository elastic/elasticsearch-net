using Newtonsoft.Json;

namespace Nest
{
	public interface IAcknowledgedResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	public abstract class AcknowledgedResponseBase : ResponseBase, IAcknowledgedResponse
	{
		[JsonProperty("acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}
