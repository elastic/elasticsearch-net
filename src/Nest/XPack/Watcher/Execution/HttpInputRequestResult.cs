using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ExactContractJsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputRequestResult>))]
	public class HttpInputRequestResult : HttpInputRequest {}
}
