using Newtonsoft.Json;

namespace Nest
{
	public interface ICloseJobResponse : IResponse
	{
		[JsonProperty("closed")]
		bool Closed { get; }
	}

	public class CloseJobResponse : ResponseBase, ICloseJobResponse
	{
		public bool Closed { get; internal set; }
	}
}
