using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IEmptyResponse : IResponse
	{
	}

	[JsonObject]
	public class EmptyResponse : BaseResponse, IEmptyResponse
	{
		public EmptyResponse()
		{
			this.IsValid = true;
		}
	}
}