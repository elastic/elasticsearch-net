using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	// TODO come up with a better name for this?
	public class ElasticsearchClientException : Exception
	{
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
