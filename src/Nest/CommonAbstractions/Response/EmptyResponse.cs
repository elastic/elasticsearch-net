using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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