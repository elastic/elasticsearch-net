using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Framework
{
	public class Auditor
	{
		private VirtualizedCluster _cluster;
		private VirtualizedCluster _clusterAsync;

		public Auditor(Func<VirtualizedCluster> setup) => Cluster = setup;

		private Auditor(VirtualizedCluster cluster, VirtualizedCluster clusterAsync)
		{
			_cluster = cluster;
			_clusterAsync = clusterAsync;
			StartedUp = true;
		}

		public Action<IConnectionPool> AssertPoolAfterCall { get; set; }
		public Action<IConnectionPool> AssertPoolAfterStartup { get; set; }

		public Action<IConnectionPool> AssertPoolBeforeCall { get; set; }
		public Action<IConnectionPool> AssertPoolBeforeStartup { get; set; }

		public List<Audit> AsyncAuditTrail { get; set; }
		public List<Audit> AuditTrail { get; set; }
		public Func<VirtualizedCluster> Cluster { get; set; }

		public IResponse Response { get; internal set; }
		public IResponse ResponseAsync { get; internal set; }

		private bool StartedUp { get; }


		public void ChangeTime(Func<DateTime, DateTime> selector)
		{
			_cluster = _cluster ?? Cluster();
			_clusterAsync = _clusterAsync ?? Cluster();

			_cluster.ChangeTime(selector);
			_clusterAsync.ChangeTime(selector);
		}

		public async Task<Auditor> TraceStartup(ClientCall callTrace = null)
		{
			//synchronous code path
			_cluster = _cluster ?? Cluster();
			if (!StartedUp) AssertPoolBeforeStartup?.Invoke(_cluster.ConnectionPool);
			AssertPoolBeforeCall?.Invoke(_cluster.ConnectionPool);
			Response = _cluster.ClientCall(callTrace?.RequestOverrides);
			AuditTrail = Response.ApiCall.AuditTrail;
			if (!StartedUp) AssertPoolAfterStartup?.Invoke(_cluster.ConnectionPool);
			AssertPoolAfterCall?.Invoke(_cluster.ConnectionPool);

			//async code path
			_clusterAsync = _clusterAsync ?? Cluster();
			if (!StartedUp) AssertPoolBeforeStartup?.Invoke(_clusterAsync.ConnectionPool);
			AssertPoolBeforeCall?.Invoke(_clusterAsync.ConnectionPool);
			ResponseAsync = await _clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			AsyncAuditTrail = ResponseAsync.ApiCall.AuditTrail;
			if (!StartedUp) AssertPoolAfterStartup?.Invoke(_clusterAsync.ConnectionPool);
			AssertPoolAfterCall?.Invoke(_clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		public async Task<Auditor> TraceCall(ClientCall callTrace, int nthCall = 0)
		{
			await TraceStartup(callTrace);
			return AssertAuditTrails(callTrace, nthCall);
		}

#pragma warning disable 1998 // Async method lacks 'await' operators and will run synchronously
		private async Task TraceException<TException>(ClientCall callTrace, Action<TException> assert)
#pragma warning restore 1998 // Async method lacks 'await' operators and will run synchronously
			where TException : ElasticsearchClientException
		{
			_cluster = _cluster ?? Cluster();
			_cluster.ClientThrows(true);
			AssertPoolBeforeCall?.Invoke(_cluster.ConnectionPool);

			Action call = () => _cluster.ClientCall(callTrace?.RequestOverrides);
			var exception = call.Should().ThrowExactly<TException>()
				.Subject.First();
			assert(exception);

			AuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_cluster.ConnectionPool);

			_clusterAsync = _clusterAsync ?? Cluster();
			_clusterAsync.ClientThrows(true);
			Func<Task> callAsync = async () => await _clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			exception = callAsync.Should().ThrowExactly<TException>()
				.Subject.First();
			assert(exception);

			AsyncAuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_clusterAsync.ConnectionPool);
		}

		public async Task<Auditor> TraceElasticsearchException(ClientCall callTrace, Action<ElasticsearchClientException> assert)
		{
			await TraceException(callTrace, assert);
			var audit = new Auditor(_cluster, _clusterAsync);
			return await audit.TraceElasticsearchExceptionOnResponse(callTrace, assert);
		}

		public async Task<Auditor> TraceUnexpectedElasticsearchException(ClientCall callTrace, Action<UnexpectedElasticsearchClientException> assert)
		{
			await TraceException(callTrace, assert);
			return new Auditor(_cluster, _clusterAsync);
		}

#pragma warning disable 1998
		public async Task<Auditor> TraceElasticsearchExceptionOnResponse(ClientCall callTrace, Action<ElasticsearchClientException> assert)
#pragma warning restore 1998
		{
			_cluster = _cluster ?? Cluster();
			_cluster.ClientThrows(false);
			AssertPoolBeforeCall?.Invoke(_cluster.ConnectionPool);

			Action call = () => { Response = _cluster.ClientCall(callTrace?.RequestOverrides); };
			call.Should().NotThrow();

			Response.ShouldNotBeValid();
			var exception = Response.ApiCall.OriginalException as ElasticsearchClientException;
			exception.Should().NotBeNull("OriginalException on response is not expected ElasticsearchClientException");
			assert(exception);

			AuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_cluster.ConnectionPool);

			_clusterAsync = _clusterAsync ?? Cluster();
			_clusterAsync.ClientThrows(false);
			Func<Task> callAsync = async () => { ResponseAsync = await _clusterAsync.ClientCallAsync(callTrace?.RequestOverrides); };
			callAsync.Should().NotThrow();
			ResponseAsync.ShouldNotBeValid();
			exception = ResponseAsync.ApiCall.OriginalException as ElasticsearchClientException;
			exception.Should().NotBeNull("OriginalException on response is not expected ElasticsearchClientException");
			assert(exception);

			AsyncAuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_clusterAsync.ConnectionPool);
			var audit = new Auditor(_cluster, _clusterAsync);

			return audit;
		}

#pragma warning disable 1998
		public async Task<Auditor> TraceUnexpectedException(ClientCall callTrace, Action<UnexpectedElasticsearchClientException> assert)
#pragma warning restore 1998
		{
			_cluster = _cluster ?? Cluster();
			AssertPoolBeforeCall?.Invoke(_cluster.ConnectionPool);

			Action call = () => _cluster.ClientCall(callTrace?.RequestOverrides);
			var exception = call.Should().ThrowExactly<UnexpectedElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			AuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_cluster.ConnectionPool);

			_clusterAsync = _clusterAsync ?? Cluster();
			Func<Task> callAsync = async () => await _clusterAsync.ClientCallAsync(callTrace?.RequestOverrides);
			exception = callAsync.Should().ThrowExactly<UnexpectedElasticsearchClientException>()
				.Subject.First();
			assert(exception);

			AsyncAuditTrail = exception.AuditTrail;
			AssertPoolAfterCall?.Invoke(_clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}

		private Auditor AssertAuditTrails(ClientCall callTrace, int nthCall)
		{
			AuditTrail.Count.Should()
				.Be(AsyncAuditTrail.Count,
					$"{nthCall} has a mismatch between sync and async. \r\nasync:{AuditTrail}\r\nsync:{AsyncAuditTrail}");

			AssertTrailOnResponse(callTrace, AuditTrail, true, nthCall);
			AssertTrailOnResponse(callTrace, AuditTrail, false, nthCall);

			callTrace?.AssertPoolAfterCall?.Invoke(_cluster.ConnectionPool);
			callTrace?.AssertPoolAfterCall?.Invoke(_clusterAsync.ConnectionPool);
			return new Auditor(_cluster, _clusterAsync);
		}


		public void VisualizeCalls(int numberOfCalls)
		{
			var cluster = _cluster ?? Cluster();
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
			foreach (var a in audits.Select((a, i) => new { a, i })) auditor = await auditor.TraceCall(a.a, a.i);
			return auditor;
		}

		private static void AssertTrailOnResponse(ClientCall callTrace, List<Audit> auditTrail, bool sync, int nthCall)
		{
			var typeOfTrail = (sync ? "synchronous" : "asynchronous") + " audit trail";
			var nthClientCall = (nthCall + 1).ToOrdinal();

			var actualAuditTrail = auditTrail.Aggregate(new StringBuilder(Environment.NewLine),
				(sb, a) => sb.AppendLine($"-> {a}"),
				sb => sb.ToString());

			callTrace.Select(c => c.Event)
				.Should()
				.ContainInOrder(auditTrail.Select(a => a.Event),
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
