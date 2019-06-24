using System;

namespace Elasticsearch.Net.Diagnostics 
{
	/// <summary> Provides a typed listener to  actions that <see cref="RequestPipeline"/> takes e.g sniff, ping, or making an API call </summary>
	public class RequestPipelineDiagnosticObserver : TypedDiagnosticObserverBase<RequestData, IApiCallDetails>
	{
		public RequestPipelineDiagnosticObserver(
			Action<(string EventName, RequestData RequestData)> onNextStart,
			Action<(string EventName, IApiCallDetails Response)> onNextEnd,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNextStart, onNextEnd, onError, onCompleted) { }

	}
}