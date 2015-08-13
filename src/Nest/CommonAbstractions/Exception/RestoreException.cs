using System;
using Elasticsearch.Net;

namespace Nest
{
	public class RestoreException : Exception
	{
		public IApiCallDetails Status { get; private set; }

		public RestoreException(IApiCallDetails status, string message = null)
			: base(message)
		{
			this.Status = status;
		}
	}
}