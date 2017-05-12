using Newtonsoft.Json;

namespace Nest
{
	public interface IDeactivateWatchResponse : IResponse
	{
		[JsonProperty("status")]
		ActivationStatus Status { get; }
	}

	public class DeactivateWatchResponse : ResponseBase, IDeactivateWatchResponse
	{
		[JsonProperty("status")]
		public ActivationStatus Status { get; internal set; }
	}
}
