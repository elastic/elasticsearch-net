// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport.Diagnostics.Auditing;

namespace Elastic.Transport.VirtualizedCluster.Audit
{
	public class CallTraceState
	{
		public CallTraceState(AuditEvent e) => Event = e;

		public Action<string, Elastic.Transport.Diagnostics.Auditing.Audit> AssertWithBecause { get; set; }

		public AuditEvent Event { get; private set; }

		public int? Port { get; set; }

		public Action<Elastic.Transport.Diagnostics.Auditing.Audit> SimpleAssert { get; set; }
	}

	public class ClientCall : List<CallTraceState>
	{
		public ClientCall() { }

		public ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides) => RequestOverrides = requestOverrides;

		public Action<IConnectionPool> AssertPoolAfterCall { get; private set; }
		public Action<ITransportResponse> AssertResponse { get; private set; }
		public Func<RequestConfigurationDescriptor, IRequestConfiguration> RequestOverrides { get; }

		public void Add(AuditEvent key, Action<Elastic.Transport.Diagnostics.Auditing.Audit> value) => Add(new CallTraceState(key) { SimpleAssert = value });

		public void Add(AuditEvent key, int port) => Add(new CallTraceState(key) { Port = port });

		public void Add(AuditEvent key) => Add(new CallTraceState(key));

		public void Add(Action<IConnectionPool> pool) => AssertPoolAfterCall = pool;

		public void Add(AuditEvent key, int port, Action<ITransportResponse> assertResponse)
		{
			Add(new CallTraceState(key) { Port = port });
			AssertResponse = assertResponse;
		}
	}
}
