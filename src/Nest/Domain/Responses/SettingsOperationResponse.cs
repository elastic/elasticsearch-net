using Newtonsoft.Json;

namespace Nest
{
    public interface ISettingsOperationResponse : IResponse
    {
        bool OK { get; }
    }

    [JsonObject]
	public class SettingsOperationResponse : BaseResponse, ISettingsOperationResponse
    {
		public SettingsOperationResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
	}
}