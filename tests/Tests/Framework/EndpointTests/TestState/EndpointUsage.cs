// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Bogus;
using Elastic.Transport.Products.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Client;

namespace Tests.Framework.EndpointTests.TestState
{
	public class EndpointUsage
	{
		private readonly object _lock = new();
		private readonly ConcurrentDictionary<string, LazyResponses> _usages = new();

		public EndpointUsage() : this("nest") { }

		protected EndpointUsage(string prefix) => CallUniqueValues = new CallUniqueValues(prefix);

		public bool CalledSetup { get; internal set; }
		public bool CalledTeardown { get; internal set; }

		public CallUniqueValues CallUniqueValues { get; }

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, string key)
		{
			// ReSharper disable once InconsistentlySynchronizedField
			if (_usages.TryGetValue(key, out var lazyResponses)) return lazyResponses;

			lock (_lock)
			{
				if (_usages.TryGetValue(key, out lazyResponses)) return lazyResponses;

				var response = clientUsage();
				_usages.TryAdd(key, response);
				return response;
			}
		}

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, int k = 0) => CallOnce(clientUsage, k.ToString());
	}

	public class SingleEndpointUsage<TResponse> : EndpointUsage
		where TResponse : class, IElasticsearchResponse
	{
		private readonly Func<string, ElasticsearchClient, TResponse> _fluent;
		private readonly Func<string, ElasticsearchClient, Task<TResponse>> _fluentAsync;
		private readonly Func<string, ElasticsearchClient, TResponse> _request;
		private readonly Func<string, ElasticsearchClient, Task<TResponse>> _requestAsync;

		public SingleEndpointUsage(
			Func<string, ElasticsearchClient, TResponse> fluent,
			Func<string, ElasticsearchClient, Task<TResponse>> fluentAsync,
			Func<string, ElasticsearchClient, TResponse> request,
			Func<string, ElasticsearchClient, Task<TResponse>> requestAsync,
			string valuePrefix = null
		) : base(valuePrefix)
		{
			_fluent = fluent;
			_fluentAsync = fluentAsync;
			_request = request;
			_requestAsync = requestAsync;
		}

		public Action<ElasticsearchClient, CallUniqueValues> IntegrationSetup { get; set; }
		public Action<ElasticsearchClient, CallUniqueValues> IntegrationTeardown { get; set; }
		public Action<ElasticsearchClient> OnAfterCall { get; set; }
		public Action<ElasticsearchClient> OnBeforeCall { get; set; }

		// ReSharper disable once StaticMemberInGenericType
		public static Randomizer Random { get; } = new(TestConfiguration.Instance.Seed);

		private LazyResponses Responses { get; set; }

		public void KickOffOnce(ElasticsearchClient client, bool oneRandomCall = false) =>
			Responses = CallOnce(() => new LazyResponses(async () =>
			{
				if (TestClient.Configuration.RunIntegrationTests)
				{
					IntegrationSetup?.Invoke(client, CallUniqueValues);
					CalledSetup = true;
				}

				var randomCall = Random.Number(0, 3);

				var dict = new Dictionary<ClientMethod, IElasticsearchResponse>();

				if (!oneRandomCall || randomCall == 0)
					Call(client, dict, ClientMethod.Fluent, v => _fluent(v, client));

				if (!oneRandomCall || randomCall == 1)
					await CallAsync(client, dict, ClientMethod.FluentAsync, v => _fluentAsync(v, client));

				if (!oneRandomCall || randomCall == 2)
					Call(client, dict, ClientMethod.Initializer, v => _request(v, client));

				if (!oneRandomCall || randomCall == 3)
					await CallAsync(client, dict, ClientMethod.InitializerAsync, v => _requestAsync(v, client));

				if (TestClient.Configuration.RunIntegrationTests)
				{
					foreach (var v in CallUniqueValues.Values.SelectMany(d => d))
						IntegrationTeardown?.Invoke(client, CallUniqueValues);
					CalledTeardown = true;
				}

				return dict;
			}));

		private void Call(ElasticsearchClient client, IDictionary<ClientMethod, IElasticsearchResponse> dict, ClientMethod method, Func<string, TResponse> call)
		{
			CallUniqueValues.CurrentView = method;
			OnBeforeCall?.Invoke(client);
			dict.Add(method, call(CallUniqueValues.Value));
			OnAfterCall?.Invoke(client);
		}

		private async Task CallAsync(ElasticsearchClient client, IDictionary<ClientMethod, IElasticsearchResponse> dict, ClientMethod method,
			Func<string, Task<TResponse>> call
		)
		{
			CallUniqueValues.CurrentView = method;
			OnBeforeCall?.Invoke(client);
			dict.Add(method, await call(CallUniqueValues.Value));
			OnAfterCall?.Invoke(client);
		}

		public async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await Responses;
			foreach (var kv in responses)
			{
				var r = kv.Value as TResponse;

				//this is to make sure any unexpected exceptions on the response are rethrown and shown during testing
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& r.ApiCall.OriginalException is not TransportException)
				{
					var e = ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException.Demystify());
					throw new ResponseAssertionException(e.SourceException, r);
				}

				try
				{
					assert(r);
				}
				catch (Exception e)
				{
					throw new ResponseAssertionException(e, r);
				}
			}
		}
	}
}
