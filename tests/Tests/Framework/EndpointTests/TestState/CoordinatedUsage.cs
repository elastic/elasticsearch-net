using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Extensions;

namespace Tests.Framework.EndpointTests.TestState
{
	public class CoordinatedUsage : KeyedCollection<string, LazyResponses>
	{
		public static readonly IResponse VoidResponse = new PingResponse();

		private readonly INestTestCluster _cluster;

		private readonly bool _testOnlyOne;

		public CoordinatedUsage(INestTestCluster cluster, EndpointUsage usage, string prefix = null, bool testOnlyOne = false)
		{
			_cluster = cluster;
			Usage = usage;
			_testOnlyOne = testOnlyOne;
			Prefix = prefix;
			_values = new Dictionary<ClientMethod, string>
			{
				{ ClientMethod.Fluent, Sanitize(RandomFluent) },
				{ ClientMethod.Initializer, Sanitize(RandomInitializer) },
				{ ClientMethod.FluentAsync, Sanitize(RandomFluentAsync) },
				{ ClientMethod.InitializerAsync, Sanitize(RandomInitializerAsync) }
			};
		}

		protected IElasticClient Client => _cluster.Client;
		private string Prefix { get; }
		private static string RandomFluent { get; } = $"f-{RandomString()}";
		private static string RandomFluentAsync { get; } = $"fa-{RandomString()}";
		private static string RandomInitializer { get; } = $"o-{RandomString()}";
		private static string RandomInitializerAsync { get; } = $"oa-{RandomString()}";

		private readonly Dictionary<ClientMethod, string> _values;
		public IReadOnlyDictionary<ClientMethod, string> MethodIsolatedValues => _values;

		public EndpointUsage Usage { get; }

		private readonly Dictionary<string, string> _callsNotInRange = new Dictionary<string, string>();

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

		public Func<string, LazyResponses> Calls<TDescriptor, TInitializer, TInterface, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync,
			Action<TResponse, CallUniqueValues> onResponse = null,
			Func<CallUniqueValues, string> uniqueValueSelector = null
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var client = Client;
			return k => Usage.CallOnce(
				() => new LazyResponses(k,
					async () => await CallAllClientMethodsOverloads(
						Usage,
						initializerBody, fluentBody, fluent, fluentAsync, request, requestAsync,
						onResponse, uniqueValueSelector,
						client
					)
				)
			, k);
		}

		public Func<string, LazyResponses> Call(Func<string, IElasticClient, Task> call) =>
			Call(async (s, c) =>
			{
				await call(s, c);

				return VoidResponse;
			});

		public Func<string, LazyResponses> Call<TResponse>(Func<string, IElasticClient, Task<TResponse>> call) where TResponse : IResponse
		{
			var client = Client;
			return k => Usage.CallOnce(
				() => new LazyResponses(k, async () =>
				{
					var dict = new Dictionary<ClientMethod, IResponse>();
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

		private async ValueTask<Dictionary<ClientMethod, IResponse>> CallAllClientMethodsOverloads<TDescriptor, TInitializer, TInterface, TResponse>(
			EndpointUsage usage,
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync,
			Action<TResponse, CallUniqueValues> onResponse,
			Func<CallUniqueValues, string> uniqueValueSelector,
			IElasticClient client
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var dict = new Dictionary<ClientMethod, IResponse>();
			async Task InvokeApiCall(
				ClientMethod method,
				Func<string, IElasticClient, ValueTask<TResponse>> invoke
				)
			{
				usage.CallUniqueValues.CurrentView = method;
				var uniqueValue = uniqueValueSelector?.Invoke(usage.CallUniqueValues) ?? _values[method];
				var response = await invoke(uniqueValue, client);
				dict.Add(method, response);
				onResponse?.Invoke(response, usage.CallUniqueValues);
			}

			await InvokeApiCall(ClientMethod.Fluent, (s, r) => new ValueTask<TResponse>(fluent(s, client, f => fluentBody(s, f))));

			if (_testOnlyOne) return dict;

			await InvokeApiCall(ClientMethod.FluentAsync, async (s, r) => await fluentAsync(s, client, f => fluentBody(s, f)));

			await InvokeApiCall(ClientMethod.Initializer, (s, r) => new ValueTask<TResponse>(request(s, client, initializerBody(s))));

			await InvokeApiCall(ClientMethod.InitializerAsync, async (s, r) => await requestAsync(s, client, initializerBody(s)));
			return dict;
		}
	}
}
