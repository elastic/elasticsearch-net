using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary> Provides a typed listener to  actions that <see cref="RequestPipeline"/> takes e.g sniff, ping, or making an API call </summary>
	public class RequestPipelineDiagnosticObserver : TypedDiagnosticObserverBase<RequestData, IApiCallDetails>
	{
		public RequestPipelineDiagnosticObserver(
			Action<KeyValuePair<string, RequestData>> onNextStart,
			Action<KeyValuePair<string, IApiCallDetails>> onNextEnd,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNextStart, onNextEnd, onError, onCompleted) { }

	}
}
