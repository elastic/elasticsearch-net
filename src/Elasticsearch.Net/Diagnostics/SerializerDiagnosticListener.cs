using System;

namespace Elasticsearch.Net.Diagnostics 
{
	/// <summary> Provides a typed listener any time an <see cref="IElasticsearchSerializer"/> does a write or read</summary>
	public class SerializerDiagnosticListener : TypedDiagnosticListenerBase<SerializerRegistrationInformation>
	{
		public SerializerDiagnosticListener(
			Action<(string EventName,  SerializerRegistrationInformation Registration)> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNext, onError, onCompleted) { }
	}
}