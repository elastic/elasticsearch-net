using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Elasticsearch.Net;

namespace Nest
{
	[Serializable]
	public class SnapshotException : Exception
	{
		public IElasticsearchResponse Status { get; private set; }

		public SnapshotException(IElasticsearchResponse status, string message)
			: base(message)
		{
			Status = status;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected SnapshotException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}
}