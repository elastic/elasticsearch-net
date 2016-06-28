using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Elasticsearch.Net;

namespace Nest
{
	[Serializable]
	public class ReindexException: Exception
	{
		public IElasticsearchResponse Status { get; private set; }

		public ReindexException(IElasticsearchResponse status, string message = null) : base(message)
		{
			this.Status = status;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ReindexException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}
}