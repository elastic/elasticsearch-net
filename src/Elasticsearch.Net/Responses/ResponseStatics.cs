using System;
using System.Collections.Generic;
using System.IO;
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
			if (r.DeprecationWarnings.HasAny())
			{
				sb.AppendLine($"# Server indicated deprecations:");
				foreach(var deprecation in r.DeprecationWarnings)
					sb.AppendLine($"- {deprecation}");
			}
			sb.AppendLine($"# Audit trail of this API call:");
			var auditTrail = (r.AuditTrail ?? Enumerable.Empty<Audit>()).ToList();
			DebugAuditTrail(auditTrail, sb);
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
				sb.AppendLine($"# Audit exception in step {a.i + 1} {a.audit.Event.GetStringValue()}:\r\n{a.audit.Exception}");
		}

		public static void DebugAuditTrail(List<Audit> auditTrail, StringBuilder sb)
		{
			if (auditTrail == null) return;
			foreach (var a in auditTrail.Select((a, i)=> new { a, i }))
			{
				var audit = a.a;
				sb.Append($" - [{a.i + 1}] {audit.Event.GetStringValue()}:");
				if (audit.Node?.Uri != null) sb.Append($" Node: {audit.Node.Uri}");
				if (audit.Exception != null) sb.Append($" Exception: {audit.Exception.GetType().Name}");
				sb.AppendLine($" Took: {(audit.Ended - audit.Started).ToString()}");
			}
		}
	}
}
