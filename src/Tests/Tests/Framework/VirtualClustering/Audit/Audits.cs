using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Tests.Framework
{
	public class CallTraceState
	{
		public CallTraceState(AuditEvent e) => Event = e;

		public Action<string, Audit> AssertWithBecause { get; set; }

		public AuditEvent Event { get; private set; }

		public int? Port { get; set; }

		public Action<Audit> SimpleAssert { get; set; }
	}

	public class ClientCall : List<CallTraceState>
	{
		public ClientCall() { }

		public ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides) => RequestOverrides = requestOverrides;

		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }
		public Func<RequestConfigurationDescriptor, IRequestConfiguration> RequestOverrides { get; }

		public void Add(AuditEvent key, Action<Audit> value) => Add(new CallTraceState(key) { SimpleAssert = value });

		public void Add(AuditEvent key, int port) => Add(new CallTraceState(key) { Port = port });

		public void Add(AuditEvent key) => Add(new CallTraceState(key));

		public void Add(Action<IConnectionPool> pool) => AssertPoolAfterCall = pool;
	}
}
