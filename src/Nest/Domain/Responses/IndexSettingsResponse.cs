using Newtonsoft.Json;

namespace Nest
{
    public interface IIndexSettingsResponse : IResponse
    {
        IndexSettings Settings { get; }
    }

    [JsonObject]
	public class IndexSettingsResponse : BaseResponse, IIndexSettingsResponse
    {
		public IndexSettings Settings { get; internal set; }
	}
}
