using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IPingResponse : IResponse
	{
	}

	[JsonObject]
	public class PingResponse : ResponseBase, IPingResponse
	{
	}
}
