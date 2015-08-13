using System;
using Elasticsearch.Net;

namespace Nest
{
	public class SnapshotException : Exception
	{
		public IApiCallDetails Status { get; private set; }

		public SnapshotException(IApiCallDetails status, string message)
			: base(message)
		{
			Status = status;
		}
	}
}