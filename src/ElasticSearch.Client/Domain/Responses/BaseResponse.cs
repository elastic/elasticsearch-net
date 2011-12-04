using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.DSL;

namespace ElasticSearch.Client
{
	public class BaseResponse
	{
		public bool IsValid { get; internal set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }

		internal PropertyNameResolver PropertyNameResolver { get; set; }
	}
}
