using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexExistsResponse : BaseResponse
	{	
		public bool Exists { get; internal set;}
	}
}
