using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	internal class Auditable : IDisposable
	{
		private readonly Audit _audit;
		private readonly Activity _activity;
		private readonly IDateTimeProvider _dateTimeProvider;

		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.RequestPipeline);

		public Auditable(AuditEvent type, List<Audit> auditTrail, IDateTimeProvider dateTimeProvider)
		{
			_dateTimeProvider = dateTimeProvider;
			var started = _dateTimeProvider.Now();

			_audit = new Audit(type, started);
			auditTrail.Add(_audit);
			_activity =
				DiagnosticSource.IsEnabled(type.GetString())
					? DiagnosticSource.StartActivity(new Activity(type.GetString()).SetStartTime(started), _audit)
					: null;
		}

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
			var ended = _dateTimeProvider.Now();
			_audit.Ended = ended;
			if (_activity != null) DiagnosticSource.StopActivity(_activity.SetEndTime(ended), _audit);
		}
	}
}
