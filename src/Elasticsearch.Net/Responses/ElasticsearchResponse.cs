using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public static class ResponseStatics
	{
		private static readonly string ResponseAlreadyCaptured = "<Response stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>";
		private static readonly string RequestAlreadyCaptured = "<Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>";
		public static string DebugInformationBuilder(IApiCallDetails r, StringBuilder sb)
		{
			sb.AppendLine($"# Audit trail of this API call:");
			var auditTrail = (r.AuditTrail ?? Enumerable.Empty<Audit>()).ToList();
			DebugAuditTrail(auditTrail, sb);
			if (r.ServerError != null) sb.AppendLine($"# ServerError: {r.ServerError}");
			if (r.OriginalException != null) sb.AppendLine($"# OriginalException: {r.OriginalException}");
			DebugAuditTrailExceptions(auditTrail, sb);

			var response = r.ResponseBodyInBytes?.Utf8String() ?? ResponseStatics.ResponseAlreadyCaptured;
			var request = r.RequestBodyInBytes?.Utf8String() ?? ResponseStatics.RequestAlreadyCaptured;
			sb.AppendLine($"# Request:\r\n{request}");
			sb.AppendLine($"# Response:\r\n{response}");

			return sb.ToString();
		}

		public static void DebugAuditTrailExceptions(List<Audit> auditTrail, StringBuilder sb)
		{
			var auditExceptions = auditTrail.Select((audit, i) => new {audit, i}).Where(a => a.audit.Exception != null);
			foreach (var a in auditExceptions)
				sb.AppendLine($"# Audit exception in step {a.i} {a.audit.Event.GetStringValue()}:\r\n{a.audit.Exception}");
		}

		public static void DebugAuditTrail(List<Audit> auditTrail, StringBuilder sb)
		{
			if (auditTrail == null) return;
			foreach (var audit in auditTrail)
			{
				sb.Append($" - {audit.Event.GetStringValue()}:");
				if (audit.Node?.Uri != null) sb.Append($" Node: {audit.Node.Uri}");
				if (audit.Exception != null) sb.Append($" Exception: {audit.Exception.GetType().Name}");
				sb.AppendLine($" Took: {(audit.Ended - audit.Started)}");
			}
		}
	}

	public class ElasticsearchResponse<T> : IApiCallDetails
	{
		public bool Success { get; }

		public HttpMethod HttpMethod { get; internal set; }

		public Uri Uri { get; internal set; }

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes { get; internal set; }

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes { get; internal set; }

		public T Body { get; protected internal set; }

		public int? HttpStatusCode { get; }

		public List<Audit> AuditTrail { get; internal set; }

		/// <summary>
		/// The response is succesful or has a response code between 400-599 the call should not be retried.
		/// Only on 502 and 503 will this return false;
		/// </summary>
		public bool SuccessOrKnownError =>
			this.Success || (HttpStatusCode >= 400 && HttpStatusCode < 599
				&& HttpStatusCode != 503 //service unavailable needs to be retried
				&& HttpStatusCode != 502 //bad gateway needs to be retried
			);

		public Exception OriginalException { get; protected internal set; }

		public ServerError ServerError { get; internal set; }

		public ElasticsearchResponse(Exception e)
		{
			this.Success = false;
			this.OriginalException = e;
		}

		public ElasticsearchResponse(int statusCode, IEnumerable<int> allowedStatusCodes)
		{
			this.Success = statusCode >= 200 && statusCode < 300 || allowedStatusCodes.Contains(statusCode);
			this.HttpStatusCode = statusCode;
		}

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.AppendLine(this.ToString());
				return ResponseStatics.DebugInformationBuilder(this, sb);
			}
		}

		public override string ToString() =>  $"{(Success ? "S" : "Uns")}uccessful low level call on {HttpMethod.GetStringValue()}: {Uri.PathAndQuery}";
	}
}
