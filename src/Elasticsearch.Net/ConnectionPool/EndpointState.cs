using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.ConnectionPool
{
	public class EndpointState
	{
		public int Attemps = -1;
		public DateTime Date = new DateTime();
	}
}
