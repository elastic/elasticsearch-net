using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Tests.Framework;

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

	public class ClientCall : List<CallTraceState>
	{
		public Func<RequestConfigurationDescriptor, IRequestConfiguration> RequestOverrides { get; }
		public ClientCall() { } 

		public ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides)
		{
			RequestOverrides = requestOverrides;
		}

		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }

		public void Add(AuditEvent key, Action<Audit> value) => this.Add(new CallTraceState(key) { SimpleAssert = value });

		public void Add(AuditEvent key, Action<string, Audit> value) => this.Add(new CallTraceState(key) { AssertWithBecause = value });

		public void Add(AuditEvent key, int port) => this.Add(new CallTraceState(key) { Port = port });

		public void Add(AuditEvent key) => this.Add(new CallTraceState(key));

		public void Add(Action<IConnectionPool> pool) => this.AssertPoolAfterCall = pool;
	}
}