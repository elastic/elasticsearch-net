using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary> Provides a typed listener to <see cref="AuditEvent"/> events that <see cref="RequestPipeline"/> emits </summary>
	public class AuditDiagnosticObserver : TypedDiagnosticObserverBase<Audit>
	{
		public AuditDiagnosticObserver(
			Action<KeyValuePair<string, Audit>> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNext, onError, onCompleted) { }
	}
}
