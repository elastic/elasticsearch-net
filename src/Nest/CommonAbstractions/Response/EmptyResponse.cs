using Newtonsoft.Json;

namespace Nest
{
	public interface IEmptyResponse : IResponse
	{
	}

	[JsonObject]
	//TODO Only used by clearscroll, does it really not return anything useful?
	public class EmptyResponse : BaseResponse, IEmptyResponse
	{
	}
}