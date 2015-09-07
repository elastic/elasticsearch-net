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
	public class Auditor
	{
		public Func<VirtualizedCluster> Cluster { get; set; }
		public Action<IConnectionPool> AssertPoolBeforeCall { get; set; }
		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }

		private VirtualizedCluster _cluster;
		private VirtualizedCluster _clusterAsync;

		public Auditor(Func<VirtualizedCluster> setup) {
			this.Cluster = setup;
		}
		private Auditor(VirtualizedCluster cluster, VirtualizedCluster clusterAsync)
		{
			_cluster = cluster;
			_clusterAsync = clusterAsync;
		}

		public ISearchResponse<Project> Response { get; internal set; }
		public ISearchResponse<Project> ResponseAsync { get; internal set; }

		public async Task<Auditor> TraceStartup(Audits audit = null)
		{
			this._cluster  = _cluster ?? this.Cluster();
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);
			this.Response = this._cluster.ClientCall();
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);
			audit?.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this.ResponseAsync = await this._clusterAsync.ClientCallAsync();
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		public async Task<Auditor> TraceCall(Audits audits)
		{
			await this.TraceStartup(audits);
			var auditTrail = this.Response.ApiCall.AuditTrail;
			var asyncAuditTrail = this.ResponseAsync.ApiCall.AuditTrail;

			auditTrail.Count.Should().Be(asyncAuditTrail.Count, "calling async should have the same audit trail length as the sync call");

			AssertTrailOnResponse(audits, auditTrail, true);
			AssertTrailOnResponse(audits, asyncAuditTrail, false);
			return new Auditor(_cluster, _clusterAsync);
		}
		public async Task<Auditor> TraceCalls(params Audits[] audits)
		{
			var auditor = this;
			foreach (var a in audits)
			{
				auditor = await auditor.TraceCall(a); 
			}
			return auditor;
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