// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Elastic.Transport.Products.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
#if NETFRAMEWORK
using Tests.Framework.Extensions;
#endif

namespace Tests.Framework.EndpointTests
{
	public class CoordinatedUsage : KeyedCollection<string, LazyResponses>
	{
		public static readonly ElasticsearchResponse VoidResponse = new PingResponse();

		private readonly ITestCluster _cluster;

		private readonly bool _testOnlyOne;

		public CoordinatedUsage(ITestCluster cluster, EndpointUsage usage, string prefix = null, bool testOnlyOne = false)
		{
			_cluster = cluster;
			_testOnlyOne = testOnlyOne;
			_values = new Dictionary<ClientMethod, string>
			{
				{ ClientMethod.Fluent, Sanitize(RandomFluent) },
				{ ClientMethod.Initializer, Sanitize(RandomInitializer) },
				{ ClientMethod.FluentAsync, Sanitize(RandomFluentAsync) },
				{ ClientMethod.InitializerAsync, Sanitize(RandomInitializerAsync) }
			};

			Usage = usage;
			Prefix = prefix;
		}

		protected ElasticsearchClient Client => _cluster.Client;
		private string Prefix { get; }
		private static string RandomFluent { get; } = $"f-{RandomString()}";
		private static string RandomFluentAsync { get; } = $"fa-{RandomString()}";
		private static string RandomInitializer { get; } = $"o-{RandomString()}";
		private static string RandomInitializerAsync { get; } = $"oa-{RandomString()}";

		private readonly Dictionary<ClientMethod, string> _values;
		public IReadOnlyDictionary<ClientMethod, string> MethodIsolatedValues => _values;

		public EndpointUsage Usage { get; }

		private readonly Dictionary<string, string> _callsNotInRange = new();

		public bool Skips(string name) => _callsNotInRange.ContainsKey(name);

		protected override string GetKeyForItem(LazyResponses item) => item.Name;

		public void Add(string name, Func<CoordinatedUsage, Func<string, LazyResponses>> create)
		{
			var responses = create(this)(name);
			Add(responses);
		}

		public void Add(string name, string versionRange, Func<CoordinatedUsage, Func<string, LazyResponses>> create)
		{
			if (!TestConfiguration.Instance.InRange(versionRange))
			{
				_callsNotInRange.Add(name, versionRange);
				return;
			}
			var responses = create(this)(name);
			Add(responses);
		}

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		public Func<string, LazyResponses> Calls<TDescriptor, TInitializer, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TDescriptor> fluentBody,
			Func<string, ElasticsearchClient, Action<TDescriptor>, TResponse> fluent,
			Func<string, ElasticsearchClient, Action<TDescriptor>, Task<TResponse>> fluentAsync,
			Func<string, ElasticsearchClient, TInitializer, TResponse> request,
			Func<string, ElasticsearchClient, TInitializer, Task<TResponse>> requestAsync,
			Action<TResponse, CallUniqueValues> onResponse = null,
			Func<CallUniqueValues, string> uniqueValueSelector = null
		)
			where TResponse : ElasticsearchResponse
			where TDescriptor : class
			where TInitializer : class
		{
			var client = Client;
			return k => Usage.CallOnce(
				() => new LazyResponses(k,
					async () => await CallAllClientMethodsOverloads(
						Usage,
						initializerBody, fluentBody, fluent, fluentAsync,
						request,
						requestAsync,
						onResponse,
						uniqueValueSelector,
						client
					)
				)
				, k);
		}

		public Func<string, LazyResponses> Call(Func<string, ElasticsearchClient, Task> call) =>
			Call(async (s, c) =>
			{
				await call(s, c);
				return VoidResponse;
			});

		public Func<string, LazyResponses> Call<TResponse>(Func<string, ElasticsearchClient, Task<TResponse>> call) where TResponse : ElasticsearchResponse
		{
			var client = Client;
			return k => Usage.CallOnce(
				() => new LazyResponses(k, async () =>
				{
					var dict = new Dictionary<ClientMethod, ElasticsearchResponse>();
					foreach (var (m, v) in _values)
					{
						var response = await call(v, client);
						dict.Add(m, response);
					}

					return dict;
				})
				, k);
		}

		private string Sanitize(string value) => string.IsNullOrEmpty(Prefix) ? value : $"{Prefix}-{value}";

		private async ValueTask<Dictionary<ClientMethod, ElasticsearchResponse>> CallAllClientMethodsOverloads<TDescriptor, TInitializer, TResponse>(
			EndpointUsage usage,
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TDescriptor> fluentBody,
			Func<string, ElasticsearchClient, Action<TDescriptor>, TResponse> fluent,
			Func<string, ElasticsearchClient, Action<TDescriptor>, Task<TResponse>> fluentAsync,
			Func<string, ElasticsearchClient, TInitializer, TResponse> request,
			Func<string, ElasticsearchClient, TInitializer, Task<TResponse>> requestAsync,
			Action<TResponse, CallUniqueValues> onResponse,
			Func<CallUniqueValues, string> uniqueValueSelector,
			ElasticsearchClient client
		)
			where TResponse : ElasticsearchResponse
			where TDescriptor : class
			where TInitializer : class
		{
			var dict = new Dictionary<ClientMethod, ElasticsearchResponse>();
			async Task InvokeApiCall(
				ClientMethod method,
				Func<string, ElasticsearchClient, ValueTask<TResponse>> invoke
			)
			{
				usage.CallUniqueValues.CurrentView = method;
				var uniqueValue = uniqueValueSelector?.Invoke(usage.CallUniqueValues) ?? _values[method];
				var response = await invoke(uniqueValue, client);
				dict.Add(method, response);
				onResponse?.Invoke(response, usage.CallUniqueValues);
			}

			await InvokeApiCall(ClientMethod.Fluent, (s, r) => new ValueTask<TResponse>(fluent(s, client, f => fluentBody(s, f))));

			if (_testOnlyOne)
				return dict;

			await InvokeApiCall(ClientMethod.FluentAsync, async (s, r) => await fluentAsync(s, client, f => fluentBody(s, f)));

			await InvokeApiCall(ClientMethod.Initializer, (s, r) => new ValueTask<TResponse>(request(s, client, initializerBody(s))));

			await InvokeApiCall(ClientMethod.InitializerAsync, async (s, r) => await requestAsync(s, client, initializerBody(s)));
			return dict;
		}
	}
}
