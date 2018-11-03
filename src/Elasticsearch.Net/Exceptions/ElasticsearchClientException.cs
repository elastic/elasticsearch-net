using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public class ElasticsearchClientException : Exception
	{
		public ElasticsearchClientException(string message) : base(message) => FailureReason = PipelineFailure.Unexpected;

		public ElasticsearchClientException(PipelineFailure failure, string message, Exception innerException)
			: base(message, innerException) => FailureReason = failure;

		public ElasticsearchClientException(PipelineFailure failure, string message, IApiCallDetails apiCall)
			: this(message)
		{
			Response = apiCall;
			FailureReason = failure;
			AuditTrail = apiCall?.AuditTrail;
		}

		public List<Audit> AuditTrail { get; internal set; }

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				var failureReason = FailureReason.GetStringValue();
				if (FailureReason == PipelineFailure.Unexpected && AuditTrail.HasAny())
					failureReason = "Unrecoverable/Unexpected " + AuditTrail.Last().Event.GetStringValue();
				var path = Request.Uri != null
					? Request.Uri.ToString()
					: Request.PathAndQuery + " on an empty node, likely a node predicate on ConnectionSettings not matching ANY nodes";

				sb.AppendLine($"# FailureReason: {failureReason} while attempting {Request.Method.GetStringValue()} on {path}");
				if (Response != null)
					ResponseStatics.DebugInformationBuilder(Response, sb);
				else
				{
					ResponseStatics.DebugAuditTrail(AuditTrail, sb);
					ResponseStatics.DebugAuditTrailExceptions(AuditTrail, sb);
				}
				if (InnerException != null)
				{
					sb.AppendLine($"# Inner Exception: {InnerException.Message}");
					sb.AppendLine(InnerException.ToString());
				}
				sb.AppendLine($"# Exception:");
				sb.AppendLine(ToString());

				return sb.ToString();
			}
		}

		public PipelineFailure? FailureReason { get; }

		public RequestData Request { get; internal set; }

		public IApiCallDetails Response { get; internal set; }
	}
}
