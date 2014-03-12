using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Exceptions
{
	public class OutOfNodesException : Exception
	{
		public OutOfNodesException(string message) : base(message)
		{
		}

		public OutOfNodesException(string message, Exception innerException) : base(message, innerException)
		{
			
		}
	}
}
