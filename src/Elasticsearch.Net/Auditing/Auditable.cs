using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	internal class Auditable : IDisposable
	{
		private readonly Audit _audit;
		private readonly IDisposable _activity;
		private readonly IDateTimeProvider _dateTimeProvider;
		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.AuditTrailEvents.SourceName);

		public Auditable(AuditEvent type, List<Audit> auditTrail, IDateTimeProvider dateTimeProvider, Node node)
		{
			_dateTimeProvider = dateTimeProvider;
			var started = _dateTimeProvider.Now();

			_audit = new Audit(type, started);
			_audit.Node = node;
			auditTrail.Add(_audit);
			var diagnosticName = type.GetAuditEventName();
			_activity = diagnosticName != null ? DiagnosticSource.Diagnose(diagnosticName, _audit) : null;
		}

		public DiagnosticAudit DiagnosticInformation => _audit;

		public AuditEvent Event
		{
			set => _audit.Event = value;
		}

		public Exception Exception
		{
			set => _audit.Exception = value;
		}

		public Node Node
		{
			set => _audit.Node = value;
		}

		public string Path
		{
			set => _audit.Path = value;
		}

		public void Dispose()
		{
			_audit.Ended = _dateTimeProvider.Now();
			_activity?.Dispose();
		}
	}
}
