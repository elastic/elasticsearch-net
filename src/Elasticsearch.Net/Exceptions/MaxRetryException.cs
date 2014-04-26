using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Exceptions
{

	/// <summary>
	/// Thrown when a request has depleeded its max retry setting
	/// </summary>
	public class MaxRetryException : Exception
	{
		public MaxRetryException(string message) : base(message)
		{
		}

		public MaxRetryException(string message, Exception innerException) : base(message, innerException)
		{
			
		}
	}
	
	
}
