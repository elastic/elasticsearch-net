using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class ElasticsearchClientException : Exception
	{
		public PipelineFailure? FailureReason { get;  }

		public RequestData Request { get; internal set; }

		public IApiCallDetails Response { get; internal set; }

		public List<Audit> AuditTrail { get; internal set; }

		public ElasticsearchClientException(string message) : base(message)
		{
			this.FailureReason = Net.PipelineFailure.Unexpected;
		}

		public ElasticsearchClientException(PipelineFailure failure, string message, Exception innerException)
			: base(message, innerException)
		{
			FailureReason = failure;
		}

		public ElasticsearchClientException(PipelineFailure failure, string message, IApiCallDetails apiCall)
			: this(message)
		{
			Response = apiCall;
			FailureReason = failure;
			AuditTrail = apiCall?.AuditTrail;

		}
	}
}
