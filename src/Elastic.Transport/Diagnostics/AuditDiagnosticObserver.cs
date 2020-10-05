// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport.Diagnostics.Auditing;

namespace Elastic.Transport.Diagnostics
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
