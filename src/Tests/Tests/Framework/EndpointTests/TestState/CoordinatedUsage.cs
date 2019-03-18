using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Framework.EndpointTests.TestState
{
	public class CoordinatedUsage : KeyedCollection<string, LazyResponses>
	{
		public static readonly IResponse VoidResponse = new PingResponse();

		private readonly INestTestCluster _cluster;
		private readonly EndpointUsage _usage;

		public CoordinatedUsage(INestTestCluster cluster, EndpointUsage usage, string prefix = null)
		{
			_cluster = cluster;
			_usage = usage;
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

		public readonly Dictionary<ClientMethod, string> _values;
		public IReadOnlyDictionary<ClientMethod, string> MethodIsolatedValues => _values;

		protected override string GetKeyForItem(LazyResponses item) => item.Name;

		public void Add(string name, Func<CoordinatedUsage, Func<string, LazyResponses>> create)
		{
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
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var client = Client;
			return k => _usage.CallOnce(
				() => new LazyResponses(k,
					async () => await CallAllClientMethodsOverloads(k, initializerBody, fluentBody, fluent, fluentAsync, request, requestAsync, client))
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
			return k => _usage.CallOnce(
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

		private async Task<Dictionary<ClientMethod, IResponse>> CallAllClientMethodsOverloads<TDescriptor, TInitializer, TInterface, TResponse>(
			string name,
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync,
			IElasticClient client
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var dict = new Dictionary<ClientMethod, IResponse>();

			var sf = _values[ClientMethod.Fluent];
			dict.Add(ClientMethod.Fluent, fluent(sf, client, f => fluentBody(sf, f)));

			var sfa = _values[ClientMethod.FluentAsync];
			dict.Add(ClientMethod.FluentAsync, await fluentAsync(sfa, client, f => fluentBody(sfa, f)));

			var si = _values[ClientMethod.Initializer];
			dict.Add(ClientMethod.Initializer, request(si, client, initializerBody(si)));

			var sia = _values[ClientMethod.InitializerAsync];
			dict.Add(ClientMethod.InitializerAsync, await requestAsync(sia, client, initializerBody(sia)));
			return dict;
		}
	}
}
