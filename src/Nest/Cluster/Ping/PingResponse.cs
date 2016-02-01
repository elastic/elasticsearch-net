using Newtonsoft.Json;

namespace Nest
{
	public interface IPingResponse : IResponse
	{
	}

	[JsonObject]
	public class PingResponse : ResponseBase, IPingResponse
	{
	}
}
