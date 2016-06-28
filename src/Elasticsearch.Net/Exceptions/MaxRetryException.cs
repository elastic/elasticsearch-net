using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Elasticsearch.Net.Exceptions
{

	/// <summary>
	/// Thrown when a request has depleeded its max retry setting
	/// </summary>
	[Serializable]
	public class MaxRetryException : Exception
	{

		public MaxRetryException(string message) : base(message) { }

		public MaxRetryException(string message, Exception innerException) : base(message, innerException) { }
		public MaxRetryException(Exception innerException) : base(innerException.Message, innerException) { }

        protected MaxRetryException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

	}
	
	/// <summary>
	/// Thrown when a sniff operation itself caused a maxrety exception
	/// </summary>
	[Serializable]
	public class SniffException : Exception
	{
		public SniffException(MaxRetryException innerException) 
			: base("Sniffing known nodes in the cluster caused a maxretry exception of its own", innerException) { }

        protected SniffException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

	}
	
	/// <summary>
	/// Thrown when a ping operation itself caused a maxrety exception
	/// </summary>
	[Serializable]
	public class PingException : Exception
	{

		public PingException(Uri baseURi, Exception innerException) 
			: base("Pinging {0} caused an exception".F(baseURi.ToString()), innerException) { }

        protected PingException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

	}
}
