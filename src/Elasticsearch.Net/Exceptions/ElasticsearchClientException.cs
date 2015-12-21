using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class ElasticsearchClientException : Exception
	{
		public RequestData Request { get; internal set; }

		public IApiCallDetails Response { get; internal set; }

		public List<Audit> AuditTrail { get; internal set; }

		public ElasticsearchClientException(string message) : base(message) { }

		public ElasticsearchClientException(string message, Exception innerException)
			: base(message, innerException) { }

		public ElasticsearchClientException(string message, IApiCallDetails apiCall)
			: this(message)
		{
			Response = apiCall;
		}
	}
}
