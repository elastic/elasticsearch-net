using System;
using System.Collections.Generic;
using System.Text;

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

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.AppendLine($"# FailureReason: {FailureReason.GetStringValue()} when trying to {Request.Method.GetStringValue()} {Request.Uri}");
				if (this.Response != null)
					ResponseStatics.DebugInformationBuilder(this.Response, sb);
				else
				{
					ResponseStatics.DebugAuditTrail(this.AuditTrail, sb);
					ResponseStatics.DebugAuditTrailExceptions(this.AuditTrail, sb);
				}
				if (InnerException != null)
				{
					sb.AppendLine($"# Inner Exception: {InnerException.Message}");
					sb.AppendLine(InnerException.ToString());
				}
				sb.AppendLine($"# Exception:");
				sb.AppendLine(this.ToString());

				return sb.ToString();
			}
		}

	}
}
