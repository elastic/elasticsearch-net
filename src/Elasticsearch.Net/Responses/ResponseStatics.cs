using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public static class ResponseStatics
	{
		private static readonly string RequestAlreadyCaptured =
			"<Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>";

		private static readonly string ResponseAlreadyCaptured =
			"<Response stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>";

		public static string DebugInformationBuilder(IApiCallDetails r, StringBuilder sb)
		{
			if (r.DeprecationWarnings.HasAny())
			{
				sb.AppendLine($"# Server indicated deprecations:");
				foreach (var deprecation in r.DeprecationWarnings)
					sb.AppendLine($"- {deprecation}");
			}
			sb.AppendLine($"# Audit trail of this API call:");
			var auditTrail = (r.AuditTrail ?? Enumerable.Empty<Audit>()).ToList();
			DebugAuditTrail(auditTrail, sb);
			if (r.OriginalException != null) sb.AppendLine($"# OriginalException: {r.OriginalException}");
			DebugAuditTrailExceptions(auditTrail, sb);

			var response = r.ResponseBodyInBytes?.Utf8String() ?? ResponseAlreadyCaptured;
			var request = r.RequestBodyInBytes?.Utf8String() ?? RequestAlreadyCaptured;
			sb.AppendLine($"# Request:\r\n{request}");
			sb.AppendLine($"# Response:\r\n{response}");

			return sb.ToString();
		}

		public static void DebugAuditTrailExceptions(List<Audit> auditTrail, StringBuilder sb)
		{
			var auditExceptions = auditTrail.Select((audit, i) => new { audit, i }).Where(a => a.audit.Exception != null);
			foreach (var a in auditExceptions)
				sb.AppendLine($"# Audit exception in step {a.i + 1} {a.audit.Event.GetStringValue()}:\r\n{a.audit.Exception}");
		}

		public static void DebugAuditTrail(List<Audit> auditTrail, StringBuilder sb)
		{
			if (auditTrail == null) return;

			foreach (var a in auditTrail.Select((a, i) => new { a, i }))
			{
				var audit = a.a;
				sb.Append($" - [{a.i + 1}] {audit.Event.GetStringValue()}:");

				AuditNodeUrl(sb, audit);

				if (audit.Exception != null) sb.Append($" Exception: {audit.Exception.GetType().Name}");
				sb.AppendLine($" Took: {(audit.Ended - audit.Started).ToString()}");
			}
		}

		private static void AuditNodeUrl(StringBuilder sb, Audit audit)
		{
			var uri = audit.Node?.Uri;
			if (uri == null) return;

			if (!string.IsNullOrEmpty(uri.UserInfo))
			{
				var builder = new UriBuilder(uri);
				builder.Password = "redacted";
				uri = builder.Uri;
			}
			sb.Append($" Node: {uri}");
		}
	}
}
