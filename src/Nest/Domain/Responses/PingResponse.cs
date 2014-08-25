using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IPingResponse : IResponse
	{
	}

	[JsonObject]
	public class PingResponse : BaseResponse, IPingResponse
	{
	}
}
