using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class IndexExistsResponse : BaseResponse
	{	
		public bool Exists { get; internal set;}
	}
}
