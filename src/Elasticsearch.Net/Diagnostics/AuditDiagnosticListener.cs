using System;

namespace Elasticsearch.Net.Diagnostics 
{
	/// <summary> Provides a typed listener to <see cref="AuditEvent"/> events that <see cref="RequestPipeline"/> emits </summary>
	public class AuditDiagnosticListener : TypedDiagnosticListenerBase<Audit>
	{
		public AuditDiagnosticListener(
			Action<(string EventName,  Audit Audit)> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNext, onError, onCompleted) { }
	}
}