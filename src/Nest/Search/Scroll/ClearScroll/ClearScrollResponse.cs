using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IClearScrollResponse : IResponse { }

	[JsonObject]
	public class ClearScrollResponse : ResponseBase, IClearScrollResponse { }
}
