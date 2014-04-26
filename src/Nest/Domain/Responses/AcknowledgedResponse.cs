using Newtonsoft.Json;

namespace Nest
{
	public interface IAcknowledgedResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	[JsonObject]
	public class AcknowledgedResponse : BaseResponse, IAcknowledgedResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}