using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	internal class Auditable : IDisposable
	{
		private readonly Audit _audit;
		private readonly IDateTimeProvider _dateTimeProvider;

		public Auditable(AuditEvent type, List<Audit> auditTrail, IDateTimeProvider dateTimeProvider)
		{
			_dateTimeProvider = dateTimeProvider;
			var started = _dateTimeProvider.Now();

			_audit = new Audit(type, started);
			auditTrail.Add(_audit);
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

		public void Dispose() => _audit.Ended = _dateTimeProvider.Now();
	}
}
