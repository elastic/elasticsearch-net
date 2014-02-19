using Newtonsoft.Json;

namespace Nest
{
	public interface ISettingsOperationResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	[JsonObject]
	public class SettingsOperationResponse : BaseResponse, ISettingsOperationResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}