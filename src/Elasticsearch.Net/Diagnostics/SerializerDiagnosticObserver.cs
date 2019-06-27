using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary> Provides a typed listener any time an <see cref="IElasticsearchSerializer"/> does a write or read</summary>
	public class SerializerDiagnosticObserver : TypedDiagnosticObserverBase<SerializerRegistrationInformation>
	{
		public SerializerDiagnosticObserver(
			Action<KeyValuePair<string, SerializerRegistrationInformation>> onNext,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNext, onError, onCompleted) { }
	}
}
