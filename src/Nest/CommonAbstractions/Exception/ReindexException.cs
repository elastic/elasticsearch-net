using System;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexException: Exception
	{
		public IApiCallDetails Status { get; private set; }

		public ReindexException(IApiCallDetails status, string message = null) : base(message)
		{
			this.Status = status;
		}
	}
}