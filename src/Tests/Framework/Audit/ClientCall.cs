using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class ClientCall
	{
		public static ClientCallAssertations OnCluster(VirtualizedCluster cluster) =>
			 new ClientCallAssertations(cluster);
	}

	public class AuditTrailTester
	{
		public Func<VirtualizedCluster> Cluster { get; set; }

		public ISearchResponse<Project> Response { get; internal set; }
		public ISearchResponse<Project> ResponseAsync { get; internal set; }
		
		public async Task Sees(Audits audits)
		{
			this.Response = this.Cluster().ClientCall();
			this.ResponseAsync = await this.Cluster().ClientCallAsync();

			var auditTrail = this.Response.ConnectionStatus.AuditTrail;
			var asyncAuditTrail = this.ResponseAsync.ConnectionStatus.AuditTrail;

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