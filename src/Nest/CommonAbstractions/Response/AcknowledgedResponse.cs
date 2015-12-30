using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAcknowledgedResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	[JsonObject]
	public abstract class AcknowledgedResponse : BaseResponse, IAcknowledgedResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}