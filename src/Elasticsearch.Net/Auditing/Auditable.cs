/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	internal class Auditable : IDisposable
	{
		private readonly Audit _audit;
		private readonly IDisposable _activity;
		private readonly IDateTimeProvider _dateTimeProvider;
		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.AuditTrailEvents.SourceName);

		public Auditable(AuditEvent type, List<Audit> auditTrail, IDateTimeProvider dateTimeProvider, Node node)
		{
			_dateTimeProvider = dateTimeProvider;
			var started = _dateTimeProvider.Now();

			_audit = new Audit(type, started) { Node = node };
			auditTrail.Add(_audit);
			var diagnosticName = type.GetAuditDiagnosticEventName();
			_activity = diagnosticName != null ? DiagnosticSource.Diagnose(diagnosticName, _audit) : null;
		}

		public AuditEvent Event
		{
			set => _audit.Event = value;
		}

		public Exception Exception
		{
			set => _audit.Exception = value;
		}

		public string Path
		{
			set => _audit.Path = value;
		}

		public void Stop() => _audit.Ended = _dateTimeProvider.Now();

		public void Dispose()
		{
			_audit.Ended = _audit.Ended == default ? _dateTimeProvider.Now() : _audit.Ended;
			_activity?.Dispose();
		}
	}
}
