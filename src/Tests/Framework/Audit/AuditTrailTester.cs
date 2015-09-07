using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Framework.MockData;
using Elasticsearch.Net.ConnectionPool;

namespace Tests.Framework
{
	public class AuditTrailTester
	{
		public Func<VirtualizedCluster> Cluster { get; set; }
		public Action<IConnectionPool> AssertPoolBeforeCall { get; set; }
		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }

		public ISearchResponse<Project> Response { get; internal set; }
		public ISearchResponse<Project> ResponseAsync { get; internal set; }

		public async Task TraceStartup()
		{
			var virtualizedCluster = this.Cluster();
			this.AssertPoolBeforeCall?.Invoke(virtualizedCluster.ConnectionPool);
			this.Response = virtualizedCluster.ClientCall();
			this.AssertPoolAfterCall?.Invoke(virtualizedCluster.ConnectionPool);

			virtualizedCluster = this.Cluster();
			this.ResponseAsync = await virtualizedCluster.ClientCallAsync();
			this.AssertPoolAfterCall?.Invoke(virtualizedCluster.ConnectionPool);
		}

		public async Task TraceCall(Audits audits)
		{
			await this.TraceStartup();
			var auditTrail = this.Response.ApiCall.AuditTrail;
			var asyncAuditTrail = this.ResponseAsync.ApiCall.AuditTrail;

			auditTrail.Count.Should().Be(asyncAuditTrail.Count, "calling async should have the same audit trail length as the sync call");

			AssertTrailOnResponse(audits, auditTrail, true);
			AssertTrailOnResponse(audits, asyncAuditTrail, false);
		}

		private static void AssertTrailOnResponse(Audits audits, List<Audit> auditTrail, bool sync)
		{
			var typeOfTrail = (sync ? "synchronous" : "asynchronous") + " audit trail";

			audits.Count().Should().Be(auditTrail.Count(), $"the test should test the whole {typeOfTrail}");
			//auditTrail.Count().Should().Be(audits.Count(), $"the test should test the whole {typeOfTrail}");
			foreach (var audit in auditTrail.Select((s, i) => new { s, i }))
			{
				var because = $"thats the type specified on the {(audit.i + 1).ToOrdinal()} {typeOfTrail}";
				audit.s.Event.Should().Be(audits[audit.i].Key, because);
				audits[audit.i].Value?.Invoke(audit.s);
			}
		}
	}
}