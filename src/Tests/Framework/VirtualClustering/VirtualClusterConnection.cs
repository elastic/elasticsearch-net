using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework.MockResponses;
using System.Net;

namespace Tests.Framework
{
	public class VirtualClusterConnection : InMemoryConnection
	{
		private static readonly object _lock = new object();
		private class State { public int Pinged = 0; public int Sniffed = 0; public int Called = 0; public int Successes = 0; public int Failures = 0; }
		private IDictionary<int, State> Calls = new Dictionary<int, State> { };

		private VirtualCluster _cluster;
		private TestableDateTimeProvider _dateTimeProvider;

		public VirtualClusterConnection(VirtualCluster cluster, TestableDateTimeProvider dateTimeProvider)
		{
			this.UpdateCluster(cluster);
			this._dateTimeProvider = dateTimeProvider;
		}

		public void UpdateCluster(VirtualCluster cluster)
		{
			if (cluster == null) return;
			lock (_lock)
			{
				this._cluster = cluster;
				this.Calls = cluster.Nodes.ToDictionary(n => n.Uri.Port, v => new State());
			}
		}

		public bool IsSniffRequest(RequestData requestData) => requestData.Path.StartsWith("_nodes/_all/settings", StringComparison.Ordinal);
		public bool IsPingRequest(RequestData requestData) => requestData.Path == "/" && requestData.Method == HttpMethod.HEAD;

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
		{
			this.Calls.Should().ContainKey(requestData.Uri.Port);
			try
			{
				var state = this.Calls[requestData.Uri.Port];
				if (IsSniffRequest(requestData))
				{
					var sniffed = Interlocked.Increment(ref state.Sniffed);
					return HandleRules<TReturn, ISniffRule>(
						requestData,
						this._cluster.SniffingRules,
						requestData.RequestTimeout,
						(r) => this.UpdateCluster(r.NewClusterState),
						(r) => SniffResponse.Create(this._cluster.Nodes, this._cluster.SniffShouldReturnFqnd)
					);
				}
				if (IsPingRequest(requestData))
				{
					var pinged = Interlocked.Increment(ref state.Pinged);
					return HandleRules<TReturn, IRule>(
						requestData,
						this._cluster.PingingRules,
						requestData.PingTimeout,
						(r) => { },
						(r) => null //HEAD request
					);
				}
				var called = Interlocked.Increment(ref state.Called);
				return HandleRules<TReturn, IClientCallRule>(
					requestData,
					this._cluster.ClientCallRules,
					requestData.RequestTimeout,
					(r) => { },
					CallResponse
				);
			}
#if DOTNETCORE
			catch (System.Net.Http.HttpRequestException e)
#else
			catch (WebException e)
#endif
			{
				var builder = new ResponseBuilder<TReturn>(requestData);
				builder.Exception = e;
				return builder.ToResponse();
			}
		}

		private ElasticsearchResponse<TReturn> HandleRules<TReturn, TRule>(
			RequestData requestData,
			IEnumerable<TRule> rules,
			TimeSpan timeout,
			Action<TRule> beforeReturn,
			Func<TRule, byte[]> successResponse
			) where TReturn : class where TRule : IRule
		{
			var state = this.Calls[requestData.Uri.Port];
			foreach (var rule in rules.Where(s => s.OnPort.HasValue))
			{
				var always = rule.Times.Match(t => true, t => false);
				var times = rule.Times.Match(t => -1, t => t);
				if (rule.OnPort.Value == requestData.Uri.Port)
				{
					if (always)
						return Always<TReturn, TRule>(requestData, timeout, beforeReturn, successResponse, rule);

					return Sometimes<TReturn, TRule>(requestData, timeout, beforeReturn, successResponse, state, rule, times);
				}
			}
			foreach (var rule in rules.Where(s => !s.OnPort.HasValue))
			{
				var always = rule.Times.Match(t => true, t => false);
				var times = rule.Times.Match(t => -1, t => t);
				if (always)
					return Always<TReturn, TRule>(requestData, timeout, beforeReturn, successResponse, rule);

				return Sometimes<TReturn, TRule>(requestData, timeout, beforeReturn, successResponse, state, rule, times);
			}
			return this.ReturnConnectionStatus<TReturn>(requestData, successResponse(default(TRule)));
		}

		private ElasticsearchResponse<TReturn> Always<TReturn, TRule>(RequestData requestData, TimeSpan timeout, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, TRule rule)
			where TReturn : class
			where TRule : IRule
		{
			if (rule.Takes.HasValue)
			{
				var time = timeout < rule.Takes.Value ? timeout: rule.Takes.Value;
				this._dateTimeProvider.ChangeTime(d=> d.Add(time));
				if (rule.Takes.Value > requestData.RequestTimeout)
#if DOTNETCORE
					throw new System.Net.Http.HttpRequestException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
#else
					throw new WebException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
#endif
			}

			return rule.Succeeds
				? Success<TReturn, TRule>(requestData, beforeReturn, successResponse, rule)
				: Fail<TReturn, TRule>(requestData, rule);
		}

		private ElasticsearchResponse<TReturn> Sometimes<TReturn, TRule>(RequestData requestData, TimeSpan timeout, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, State state, TRule rule, int times)
			where TReturn : class
			where TRule : IRule
		{
			if (rule.Takes.HasValue)
			{
				var time = timeout < rule.Takes.Value ? timeout : rule.Takes.Value;
				this._dateTimeProvider.ChangeTime(d=> d.Add(time));
				if (rule.Takes.Value > requestData.RequestTimeout)
#if DOTNETCORE
					throw new System.Net.Http.HttpRequestException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
#else
					throw new WebException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
#endif
			}

			if (rule.Succeeds && times >= state.Successes)
				return Success<TReturn, TRule>(requestData, beforeReturn, successResponse, rule);
			else if (rule.Succeeds) return Fail<TReturn, TRule>(requestData, rule);

			if (!rule.Succeeds && times >= state.Failures)
				return Fail<TReturn, TRule>(requestData, rule);
			return Success<TReturn, TRule>(requestData, beforeReturn, successResponse, rule);
		}

		private ElasticsearchResponse<TReturn> Fail<TReturn, TRule>(RequestData requestData, TRule rule)
			where TReturn : class
			where TRule : IRule
		{
			var state = this.Calls[requestData.Uri.Port];
			var failed = Interlocked.Increment(ref state.Failures);
			if (rule.Return == null)
#if DOTNETCORE
				throw new System.Net.Http.HttpRequestException();
#else
				throw new WebException();
#endif
			return rule.Return.Match(
				(e) =>
				{
					throw e;
				},
				(statusCode) => this.ReturnConnectionStatus<TReturn>(requestData, CallResponse(rule), statusCode)
			);
		}

		private ElasticsearchResponse<TReturn> Success<TReturn, TRule>(RequestData requestData, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, TRule rule)
			where TReturn : class
			where TRule : IRule
		{
			var state = this.Calls[requestData.Uri.Port];
			var succeeded = Interlocked.Increment(ref state.Successes);
			beforeReturn?.Invoke(rule);
			return this.ReturnConnectionStatus<TReturn>(requestData, successResponse(rule));
		}

		private byte[] CallResponse<TRule>(TRule rule)
			where TRule : IRule
		{
			if (rule?.ReturnResponse != null)
				return rule.ReturnResponse;

			var response = DefaultResponse;
			using (var ms = new MemoryStream())
			{
				new ElasticsearchDefaultSerializer().Serialize(response, ms);
				return ms.ToArray();
			}
		}

		private static object DefaultResponse
		{
			get
			{
				var response = new
				{
					name = "Razor Fist",
					cluster_name = "elasticsearch-test-cluster",
					version = new
					{
						number = "2.0.0",
						build_hash = "af1dc6d8099487755c3143c931665b709de3c764",
						build_timestamp = "2015-07-07T11:28:47Z",
						build_snapshot = true,
						lucene_version = "5.2.1"
					},
					tagline = "You Know, for Search"
				};
				return response;
			}
		}

		public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData)
		{
			return Task.FromResult(this.Request<TReturn>(requestData));
		}
	}
}
