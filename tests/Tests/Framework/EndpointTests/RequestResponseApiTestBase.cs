// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Serialization;
using Tests.Domain.Helpers;
using Tests.Framework.EndpointTests.TestState;
using Xunit;

namespace Tests.Framework.EndpointTests
{
	public abstract class RequestResponseApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ExpectJsonTestBase, IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TResponse : class, IResponse
		where TInterface : class
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
	{
		private readonly EndpointUsage _usage;

		protected RequestResponseApiTestBase(TCluster cluster, EndpointUsage usage) : base(cluster.Client)
		{
			_usage = usage ?? throw new ArgumentNullException(nameof(usage));

			if (cluster == null) throw new ArgumentNullException(nameof(cluster));

			Cluster = cluster;
			Responses = usage.CallOnce(ClientUsage);
			UniqueValues = usage.CallUniqueValues;
		}

		public virtual IElasticClient Client =>
			TestConfiguration.Instance.RunIntegrationTests ? Cluster.Client : TestClient.DefaultInMemoryClient;

		public TCluster Cluster { get; }

		protected virtual string CallIsolatedValue => UniqueValues.Value;
		protected virtual Func<TDescriptor, TInterface> Fluent { get; } = null;
		protected virtual TInitializer Initializer { get; } = null;
		protected bool RanIntegrationSetup => _usage?.CalledSetup ?? false;
		protected LazyResponses Responses { get; }

		protected virtual bool TestOnlyOne => TestClient.Configuration.TestOnlyOne;

		protected CallUniqueValues UniqueValues { get; }

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected string U(string s) => Uri.EscapeDataString(s);

		protected string Q(string s) => Uri.EscapeUriString(s);

		protected T ExtendedValue<T>(string key) where T : class => UniqueValues.ExtendedValue<T>(key);

		protected bool TryGetExtendedValue<T>(string key, out T t) where T : class => UniqueValues.TryGetExtendedValue(key, out t);

		protected void ExtendedValue<T>(string key, T value) where T : class => UniqueValues.ExtendedValue(key, value);

		protected virtual TDescriptor NewDescriptor() => Activator.CreateInstance<TDescriptor>();

		protected virtual void IntegrationSetup(IElasticClient client, CallUniqueValues values) { }

		protected virtual void IntegrationTeardown(IElasticClient client, CallUniqueValues values) { }

		protected virtual void OnBeforeCall(IElasticClient client) { }

		protected virtual void OnAfterCall(IElasticClient client) { }

		protected abstract LazyResponses ClientUsage();

		protected LazyResponses Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		) => new LazyResponses(async () =>
		{
			var client = Client;

			void IntegrateOnly(Action<IElasticClient> act)
			{
				if (!TestClient.Configuration.RunIntegrationTests) return;

				act(client);
			}

			if (TestClient.Configuration.RunIntegrationTests)
			{
				IntegrationSetup(client, UniqueValues);
				_usage.CalledSetup = true;
			}

			(ClientMethod, Func<ValueTask<TResponse>>) Api(ClientMethod method, Func<ValueTask<TResponse>> action) => (method, action);

			var dict = new Dictionary<ClientMethod, IResponse>();
			var views = new[]
			{
				Api(ClientMethod.Fluent, () => new ValueTask<TResponse>(fluent(client, Fluent))),
				Api(ClientMethod.Initializer, () => new ValueTask<TResponse>(request(client, Initializer))),
				Api(ClientMethod.FluentAsync, async () => await fluentAsync(client, Fluent)),
				Api(ClientMethod.InitializerAsync, async () => await requestAsync(client, Initializer)),
			};
			foreach (var (v, m) in views.OrderBy((t) => Gimme.Random.Int()))
			{
				UniqueValues.CurrentView = v;

				IntegrateOnly(OnBeforeCall);
				dict.Add(v, await m());
				IntegrateOnly(OnAfterCall);
				if (TestOnlyOne) break;
			}

			if (TestClient.Configuration.RunIntegrationTests)
			{
				IntegrationTeardown(client, UniqueValues);
				_usage.CalledTeardown = true;
			}

			return dict;
		});

		protected virtual async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await Responses;
			foreach (var kv in responses)
			{
				var response = kv.Value as TResponse;
				try
				{
					UniqueValues.CurrentView = kv.Key;
					assert(response);
				}
#pragma warning disable 7095 //enable this if you expect a single overload to act up
#pragma warning disable 8360
				catch (Exception ex) when (false)
#pragma warning restore 7095
#pragma warning restore 8360
#pragma warning disable 0162 //dead code while the previous exception filter is false
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
#pragma warning restore 0162
			}
		}
	}
}
