// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary> Provides a typed listener to the events that <see cref="HttpConnection"/> emits </summary>
	public class HttpConnectionDiagnosticObserver : TypedDiagnosticObserverBase<RequestData, int?>
	{
		public HttpConnectionDiagnosticObserver(
			Action<KeyValuePair<string, RequestData>> onNextStart,
			Action<KeyValuePair<string, int?>> onNextEnd,
			Action<Exception> onError = null,
			Action onCompleted = null
		) : base(onNextStart, onNextEnd, onError, onCompleted) { }

	}
}
