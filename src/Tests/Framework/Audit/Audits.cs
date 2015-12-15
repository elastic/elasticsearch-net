using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Tests.Framework
{
	public class CallTraceState
	{
		public CallTraceState(AuditEvent e) { this.Event = e;  }

		public AuditEvent Event { get; private set; }

		public int? Port { get; set; }

		public Action<Audit> SimpleAssert { get; set; }
		
		public Action<string, Audit> AssertWithBecause { get; set; }
	}

	public class CallTrace : List<CallTraceState>
	{
		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }

		public void Add(AuditEvent key, Action<Audit> value) => this.Add(new CallTraceState(key) { SimpleAssert = value });

		public void Add(AuditEvent key, Action<string, Audit> value) => this.Add(new CallTraceState(key) { AssertWithBecause = value });

		public void Add(AuditEvent key, int port) => this.Add(new CallTraceState(key) { Port = port });

		public void Add(AuditEvent key) => this.Add(new CallTraceState(key));

		public void Add(Action<IConnectionPool> pool) => this.AssertPoolAfterCall = pool;
	}
}