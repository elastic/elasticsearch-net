using System;

namespace Elasticsearch.Net.Diagnostics 
{
	/// <summary> Provides a typed listener any time an <see cref="IElasticsearchSerializer"/> does a write or read</summary>
	public class SerializerDiagnosticObserver : TypedDiagnosticObserverBase<SerializerRegistrationInformation>
	{
		public SerializerDiagnosticObserver(
			Action<(string EventName,  SerializerRegistrationInformation Registration)> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNext, onError, onCompleted) { }
	}
}