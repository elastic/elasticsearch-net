using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class IndexExistsResponse
	{
		public bool IsValid { get; internal set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }
		
		public bool Exists { get; internal set;}
	}
}
