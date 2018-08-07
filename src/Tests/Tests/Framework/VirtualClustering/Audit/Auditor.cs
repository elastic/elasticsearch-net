using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Framework
{
	public class Auditor
	{
		public Func<VirtualizedCluster> Cluster { get; set; }
		public Action<IConnectionPool> AssertPoolBeforeStartup { get; set; }
		public Action<IConnectionPool> AssertPoolAfterStartup { get; set; }

		public Action<IConnectionPool> AssertPoolBeforeCall { get; set; }
		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }

		private VirtualizedCluster _cluster;
		private VirtualizedCluster _clusterAsync;

		private bool StartedUp { get; }

		public Auditor(Func<VirtualizedCluster> setup) {
			this.Cluster = setup;
		}
		private Auditor(VirtualizedCluster cluster, VirtualizedCluster clusterAsync)
		{
			_cluster = cluster;
			_clusterAsync = clusterAsync;
			this.StartedUp = true;
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
			//synchronous code path
			this._cluster  = _cluster ?? this.Cluster();
			if (!this.StartedUp) this.AssertPoolBeforeStartup?.Invoke(this._cluster.ConnectionPool);
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);
			this.Response = this._cluster.ClientCall(callTrace?.RequestOverrides);
			this.AuditTrail = this.Response.ApiCall.AuditTrail;
			if (!this.StartedUp) this.AssertPoolAfterStartup?.Invoke(this._cluster.ConnectionPool);
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			//async code path
			this._clusterAsync = _clusterAsync ?? this.Cluster();
			if (!this.StartedUp) this.AssertPoolBeforeStartup?.Invoke(this._clusterAsync.ConnectionPool);
			this.AssertPoolBeforeCall?.Invoke(this._clusterAsync.ConnectionPool);
			this.ResponseAsync = await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			this.AsyncAuditTrail = this.ResponseAsync.ApiCall.AuditTrail;
			if (!this.StartedUp) this.AssertPoolAfterStartup?.Invoke(this._clusterAsync.ConnectionPool);
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		public async Task<Auditor> TraceCall(ClientCall callTrace, int nthCall = 0)
		{
			await this.TraceStartup(callTrace);
			return AssertAuditTrails(callTrace, nthCall);
		}

#pragma warning disable 1998 // Async method lacks 'await' operators and will run synchronously
		private async Task TraceException<TException>(ClientCall callTrace, Action<TException> assert)
#pragma warning restore 1998 // Async method lacks 'await' operators and will run synchronously
			where TException : ElasticsearchClientException
		{
			this._cluster  = _cluster ?? this.Cluster();
			this._cluster.ClientThrows(true);
			this.AssertPoolBeforeCall?.Invoke(this._cluster.ConnectionPool);

			Action call = () => this._cluster.ClientCall(callTrace?.RequestOverrides);
			var exception = call.ShouldThrowExactly<TException>()
				.Subject.First();
			assert(exception);

			this.AuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this._clusterAsync.ClientThrows(true);
			Func<Task> callAsync = async () => await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			exception = callAsync.ShouldThrowExactly<TException>()
				.Subject.First();
			assert(exception);

			this.AsyncAuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
		}
		public async Task<Auditor> TraceElasticsearchException(ClientCall callTrace, Action<ElasticsearchClientException> assert)
		{
			await this.TraceException(callTrace, assert);
			var audit  = new Auditor(_cluster, _clusterAsync);
			return await audit.TraceElasticsearchExceptionOnResponse(callTrace, assert);
		}

		public async Task<Auditor> TraceUnexpectedElasticsearchException(ClientCall callTrace, Action<UnexpectedElasticsearchClientException> assert)
		{
			await this.TraceException(callTrace, assert);
			return new Auditor(_cluster, _clusterAsync);
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

			this.Response.ShouldNotBeValid();
			var exception = this.Response.ApiCall.OriginalException as ElasticsearchClientException;
			exception.Should().NotBeNull("OriginalException on response is not expected ElasticsearchClientException");
			assert(exception);

			this.AuditTrail = exception.AuditTrail;
			this.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);

			this._clusterAsync = _clusterAsync ?? this.Cluster();
			this._clusterAsync.ClientThrows(false);
			Func<Task> callAsync = async () => { this.ResponseAsync = await this._clusterAsync.ClientCallAsync(callTrace?.RequestOverrides); };
			callAsync.ShouldNotThrow();
			this.ResponseAsync.ShouldNotBeValid();
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
				.Be(this.AsyncAuditTrail.Count,
					$"{nthCall} has a mismatch between sync and async. \r\nasync:{this.AuditTrail}\r\nsync:{this.AsyncAuditTrail}");

			AssertTrailOnResponse(callTrace, this.AuditTrail, true, nthCall);
			AssertTrailOnResponse(callTrace, this.AuditTrail, false, nthCall);

			callTrace?.AssertPoolAfterCall?.Invoke(this._cluster.ConnectionPool);
			callTrace?.AssertPoolAfterCall?.Invoke(this._clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}


		public void VisualizeCalls(int numberOfCalls)
		{
			var cluster  = _cluster ?? this.Cluster();
			var messages = new List<string>(numberOfCalls * 2);
			for (var i = 0; i < numberOfCalls; i++)
			{
				var call = cluster.ClientCall();
				var d = call.ApiCall;
                var actualAuditTrail = AuditTrailToString(d.AuditTrail);
				messages.Add($"{d.HttpMethod.GetStringValue()} ({d.Uri.Port})");
				messages.Add(actualAuditTrail);
			}
			throw new Exception(string.Join(Environment.NewLine, messages));
		}

		private static string AuditTrailToString(List<Audit> auditTrail)
		{
			var actualAuditTrail = auditTrail.Aggregate(new StringBuilder(),
				(sb, a) => sb.AppendLine($"-> {a}"),
				sb => sb.ToString());
			return actualAuditTrail;
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

			var actualAuditTrail = auditTrail.Aggregate(new StringBuilder(Environment.NewLine),
				(sb, a)=> sb.AppendLine($"-> {a}"),
				sb => sb.ToString());

			callTrace.Select(c=>c.Event)
				.Should().ContainInOrder(auditTrail.Select(a=>a.Event),
					$"the {nthClientCall} client call's {typeOfTrail} should assert ALL audit trail items{actualAuditTrail}");

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

			callTrace.Count.Should().Be(auditTrail.Count, $"actual auditTrail {actualAuditTrail}");
		}

	}

}
