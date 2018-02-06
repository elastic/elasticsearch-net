using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.MockResponses;

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

		public bool IsSniffRequest(RequestData requestData) => requestData.PathAndQuery.StartsWith(RequestPipeline.SniffPath, StringComparison.Ordinal);
		public bool IsPingRequest(RequestData requestData) => requestData.PathAndQuery == "/" && requestData.Method == HttpMethod.HEAD;

		public override TResponse Request<TResponse>(RequestData requestData)
		{
			this.Calls.Should().ContainKey(requestData.Uri.Port);
			try
			{
				var state = this.Calls[requestData.Uri.Port];
				if (IsSniffRequest(requestData))
				{
					var sniffed = Interlocked.Increment(ref state.Sniffed);
					return HandleRules<TResponse, ISniffRule>(
						requestData,
						this._cluster.SniffingRules,
						requestData.RequestTimeout,
						(r) => this.UpdateCluster(r.NewClusterState),
						(r) => SniffResponseBytes.Create(this._cluster.Nodes, this._cluster.PublishAddressOverride, this._cluster.SniffShouldReturnFqnd)
					);
				}
				if (IsPingRequest(requestData))
				{
					var pinged = Interlocked.Increment(ref state.Pinged);
					return HandleRules<TResponse, IRule>(
						requestData,
						this._cluster.PingingRules,
						requestData.PingTimeout,
						(r) => { },
						(r) => null //HEAD request
					);
				}
				var called = Interlocked.Increment(ref state.Called);
				return HandleRules<TResponse, IClientCallRule>(
					requestData,
					this._cluster.ClientCallRules,
					requestData.RequestTimeout,
					(r) => { },
					CallResponse
				);
			}
			catch (System.Net.Http.HttpRequestException e)
			{
				return ResponseBuilder.ToResponse<TResponse>(requestData, e, null, null, Stream.Null);
			}
		}

		private TResponse HandleRules<TResponse, TRule>(
			RequestData requestData,
			IEnumerable<TRule> rules,
			TimeSpan timeout,
			Action<TRule> beforeReturn,
			Func<TRule, byte[]> successResponse
			)
			where TResponse : class, IElasticsearchResponse, new()
			where TRule : IRule
		{
			requestData.MadeItToResponse = true;

			var state = this.Calls[requestData.Uri.Port];
			foreach (var rule in rules.Where(s => s.OnPort.HasValue))
			{
				var always = rule.Times.Match(t => true, t => false);
				var times = rule.Times.Match(t => -1, t => t);
				if (rule.OnPort.Value == requestData.Uri.Port)
				{
					if (always)
						return Always<TResponse, TRule>(requestData, timeout, beforeReturn, successResponse, rule);

					return Sometimes<TResponse, TRule>(requestData, timeout, beforeReturn, successResponse, state, rule, times);
				}
			}
			foreach (var rule in rules.Where(s => !s.OnPort.HasValue))
			{
				var always = rule.Times.Match(t => true, t => false);
				var times = rule.Times.Match(t => -1, t => t);
				if (always)
					return Always<TResponse, TRule>(requestData, timeout, beforeReturn, successResponse, rule);

				return Sometimes<TResponse, TRule>(requestData, timeout, beforeReturn, successResponse, state, rule, times);
			}
			return this.ReturnConnectionStatus<TResponse>(requestData, successResponse(default(TRule)));
		}

		private TResponse Always<TResponse, TRule>(RequestData requestData, TimeSpan timeout, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, TRule rule)
			where TResponse : class, IElasticsearchResponse, new()
			where TRule : IRule
		{
			if (rule.Takes.HasValue)
			{
				var time = timeout < rule.Takes.Value ? timeout: rule.Takes.Value;
				this._dateTimeProvider.ChangeTime(d=> d.Add(time));
				if (rule.Takes.Value > requestData.RequestTimeout)
					throw new System.Net.Http.HttpRequestException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
			}

			return rule.Succeeds
				? Success<TResponse, TRule>(requestData, beforeReturn, successResponse, rule)
				: Fail<TResponse, TRule>(requestData, rule);
		}

		private TResponse Sometimes<TResponse, TRule>(
			RequestData requestData, TimeSpan timeout, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, State state, TRule rule, int times)
			where TResponse : class, IElasticsearchResponse, new()
			where TRule : IRule
		{
			if (rule.Takes.HasValue)
			{
				var time = timeout < rule.Takes.Value ? timeout : rule.Takes.Value;
				this._dateTimeProvider.ChangeTime(d=> d.Add(time));
				if (rule.Takes.Value > requestData.RequestTimeout)
					throw new System.Net.Http.HttpRequestException($"Request timed out after {time} : call configured to take {rule.Takes.Value} while requestTimeout was: {timeout}");
			}

			if (rule.Succeeds && times >= state.Successes)
				return Success<TResponse, TRule>(requestData, beforeReturn, successResponse, rule);
			else if (rule.Succeeds)
			{
				return Fail<TResponse, TRule>(requestData, rule);
			}

			if (!rule.Succeeds && times >= state.Failures)
				return Fail<TResponse, TRule>(requestData, rule);
			return Success<TResponse, TRule>(requestData, beforeReturn, successResponse, rule);
		}

		private TResponse Fail<TResponse, TRule>(RequestData requestData, TRule rule, Union<Exception, int> returnOverride = null)
			where TResponse : class, IElasticsearchResponse, new()
			where TRule : IRule
		{
			var state = this.Calls[requestData.Uri.Port];
			var failed = Interlocked.Increment(ref state.Failures);
			var ret = returnOverride ?? rule.Return;

			if (ret == null)
				throw new System.Net.Http.HttpRequestException();
			return ret.Match(
				(e) => throw e,
				(statusCode) => this.ReturnConnectionStatus<TResponse>(requestData, CallResponse(rule),
					//make sure we never return a valid status code in Fail responses because of a bad rule.
					statusCode >= 200 && statusCode < 300 ? 502 : statusCode, rule.ReturnContentType)
			);
		}

		private TResponse Success<TResponse, TRule>(RequestData requestData, Action<TRule> beforeReturn, Func<TRule, byte[]> successResponse, TRule rule)
			where TResponse : class, IElasticsearchResponse, new()
			where TRule : IRule
		{
			var state = this.Calls[requestData.Uri.Port];
			var succeeded = Interlocked.Increment(ref state.Successes);
			beforeReturn?.Invoke(rule);
			return this.ReturnConnectionStatus<TResponse>(requestData, successResponse(rule), contentType: rule.ReturnContentType);
		}

		private static byte[] CallResponse<TRule>(TRule rule)
			where TRule : IRule
		{
			if (rule?.ReturnResponse != null)
				return rule.ReturnResponse;

			if (DefaultResponseBytes != null) return DefaultResponseBytes;
			var response = DefaultResponse;
			using (var ms = new MemoryStream())
			{
				new LowLevelRequestResponseSerializer().Serialize(response, ms);
				DefaultResponseBytes = ms.ToArray();
			}
			return DefaultResponseBytes;
		}

		private static byte[] DefaultResponseBytes;
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

		public override Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
		{
			return Task.FromResult(this.Request<TResponse>(requestData));
		}
	}
}
