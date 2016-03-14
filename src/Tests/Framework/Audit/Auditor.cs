using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.MockData;

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

		public IResponse Response { get; internal set; }
		public IResponse ResponseAsync { get; internal set; }

		public List<Audit> AsyncAuditTrail { get; set; }
		public List<Audit> AuditTrail { get; set; }

		public void ChangeTime(Func<DateTime, DateTime> selector)
		{
			this._cluster  = _cluster ?? this.Cluster();
			this._clusterAsync = _clusterAsync ?? this.Cluster();

			this._cluster.ChangeTime(selector);
			this._clusterAsync.ChangeTime(selector);
		}

		public async Task<Auditor> TraceStartup(ClientCall callTrace = null)
		{
			this._cluster  = _cluster ?? this.Cluster();
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);
			this.Response = this._cluster.ClientCall(callTrace?.RequestOverrides);
			this.AuditTrail = this.Response.ApiCall.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this.ResponseAsync = await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			this.AsyncAuditTrail = this.ResponseAsync.ApiCall.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		public async Task<Auditor> TraceCall(ClientCall callTrace, int nthCall = 0)
		{
			await this.TraceStartup(callTrace);
			return AssertAuditTrails(callTrace, nthCall);
		}

		public async Task<Auditor> TraceElasticsearchException(ClientCall callTrace, Action<ElasticsearchClientException> assert)
		{
			this._cluster  = _cluster ?? this.Cluster();
			this._cluster.ClientThrows(true);
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);

			Action call = () => this._cluster.ClientCall(callTrace?.RequestOverrides);
			var exception = call.ShouldThrowExactly<ElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			this.AuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this._clusterAsync.ClientThrows(true);
			Func<Task> callAsync = async () => await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			exception = callAsync.ShouldThrowExactly<ElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			this.AsyncAuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			var audit  = new Auditor(_cluster, _clusterAsync);
			return await audit.TraceElasticsearchExceptionOnResponse(callTrace, assert);
		}

#pragma warning disable 1998
		public async Task<Auditor> TraceElasticsearchExceptionOnResponse(ClientCall callTrace, Action<ElasticsearchClientException> assert)
#pragma warning restore 1998
		{
			this._cluster  = _cluster ?? this.Cluster();
			this._cluster.ClientThrows(false);
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);

			Action call = () => { this.Response = this._cluster.ClientCall(callTrace?.RequestOverrides); };
			call.ShouldNotThrow();

			this.Response.IsValid.Should().BeFalse();
			var exception = this.Response.ApiCall.OriginalException as ElasticsearchClientException;
			exception.Should().NotBeNull("OriginalException on response is not expected ElasticsearchClientException");
			assert(exception);

			this.AuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this._clusterAsync.ClientThrows(false);
			Func<Task> callAsync = async () => { this.ResponseAsync = await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides); };
			callAsync.ShouldNotThrow();
			this.ResponseAsync.IsValid.Should().BeFalse();
			exception = this.ResponseAsync.ApiCall.OriginalException as ElasticsearchClientException;
			exception.Should().NotBeNull("OriginalException on response is not expected ElasticsearchClientException");
			assert(exception);

			this.AsyncAuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			var audit  = new Auditor(_cluster, _clusterAsync);

			return audit;
		}

#pragma warning disable 1998
		public async Task<Auditor> TraceUnexpectedException(ClientCall callTrace, Action<UnexpectedElasticsearchClientException> assert)
#pragma warning restore 1998
		{
			this._cluster  = _cluster ?? this.Cluster();
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);

			Action call = () => this._cluster.ClientCall(callTrace?.RequestOverrides);
			var exception = call.ShouldThrowExactly<UnexpectedElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			this.AuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			Func<Task> callAsync = async () => await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			exception = callAsync.ShouldThrowExactly<UnexpectedElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			this.AsyncAuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		private Auditor AssertAuditTrails(ClientCall callTrace, int nthCall)
		{
			this.AuditTrail.Count.Should()
				.Be(this.AsyncAuditTrail.Count, "calling async should have the same audit trail length as the sync call");

			AssertTrailOnResponse(callTrace, this.AuditTrail, true, nthCall);
			AssertTrailOnResponse(callTrace, this.AuditTrail, false, nthCall);

			callTrace?.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);
			callTrace?.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}


		public async Task<Auditor> TraceCalls(params ClientCall[] audits)
		{
			var auditor = this;
			foreach (var a in audits.Select((a, i)=> new { a, i }))
			{
				auditor = await auditor.TraceCall(a.a, a.i);
			}
			return auditor;
		}

		private static void AssertTrailOnResponse(ClientCall callTrace, List<Audit> auditTrail, bool sync, int nthCall)
		{
			var typeOfTrail = (sync ? "synchronous" : "asynchronous") + " audit trail";
			var nthClientCall = (nthCall + 1).ToOrdinal();

			callTrace.Select(c=>c.Event).Should().ContainInOrder(auditTrail.Select(a=>a.Event), $"the {nthClientCall} client call's {typeOfTrail} should assert ALL audit trail items");
			foreach (var t in auditTrail.Select((a, i) => new { a, i }))
			{
				var i = t.i;
				var audit = t.a;
				var nthAuditTrailItem = (i + 1).ToOrdinal();
				var because = $"thats the {{0}} specified on the {nthAuditTrailItem} item in the {nthClientCall} client call's {typeOfTrail}";
				var c = callTrace[i];
				audit.Event.Should().Be(c.Event, string.Format(because, "event"));
				if (c.Port.HasValue) audit.Node.Uri.Port.Should().Be(c.Port.Value, string.Format(because, "port"));
				c.SimpleAssert?.Invoke(audit);
				c.AssertWithBecause?.Invoke(string.Format(because, "custom assertion"), audit);
			}

			callTrace.Count.Should().Be(auditTrail.Count);
		}
	}
}
