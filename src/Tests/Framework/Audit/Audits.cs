using Elasticsearch.Net.Connection;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Framework;
using Pair = System.Collections.Generic.KeyValuePair<Elasticsearch.Net.Connection.AuditEvent, System.Action<Elasticsearch.Net.Connection.Audit>>;
using Elasticsearch.Net.ConnectionPool;

namespace Tests.Framework
{
	public class Audits : List<Pair>
	{

		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }


		public void Add(AuditEvent key, Action<Audit> value) => this.Add(new Pair(key, value));

		public void Add(AuditEvent key) => this.Add(new Pair(key, null));
		public void Add(Action<IConnectionPool> pool) => this.AssertPoolAfterCall = pool;

		public void Add(AuditEvent key, int port)
		{
			var because = $"thats the port specified on the {(this.Count() + 1).ToOrdinal()} audit";
			this.Add(new Pair(key, a => a.Node.Uri.Port.Should().Be(port, because)));
		}

	}
}