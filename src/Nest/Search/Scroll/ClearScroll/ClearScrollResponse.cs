using Newtonsoft.Json;

namespace Nest
{
	public interface IClearScrollResponse : IResponse
	{
	}

	[JsonObject]
	public class ClearScrollResponse : ResponseBase, IClearScrollResponse
	{
	}
}