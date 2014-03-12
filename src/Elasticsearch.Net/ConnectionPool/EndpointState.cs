using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.ConnectionPool
{
	public class EndpointState
	{
		public int _attempts = -1;
		public DateTime date = new DateTime();
	}
}
