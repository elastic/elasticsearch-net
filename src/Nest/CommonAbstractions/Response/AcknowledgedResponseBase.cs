using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAcknowledgedResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	[JsonObject]
	public abstract class AcknowledgedResponseBase : ResponseBase, IAcknowledgedResponse
	{
		[JsonProperty("acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}
