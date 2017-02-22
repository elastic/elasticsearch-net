using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputRequestResult>))]
	public class HttpInputRequestResult : HttpInputRequest {}
}
