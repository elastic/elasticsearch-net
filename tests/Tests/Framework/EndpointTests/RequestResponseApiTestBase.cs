// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Transport.Products.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Serialization;
using Tests.Domain.Helpers;
using Tests.Framework.EndpointTests.TestState;
#if NETFRAMEWORK
using Tests.Framework.Extensions;
#endif
using Xunit;

namespace Tests.Framework.EndpointTests
{
	public abstract class RequestResponseApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		: ExpectJsonTestBase, IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
		where TResponse : ElasticsearchResponse
		where TDescriptor : class
		where TInitializer : class
	{
		private readonly EndpointUsage _usage;

		protected RequestResponseApiTestBase(TCluster cluster, EndpointUsage usage)  : base(cluster.Client)
		{
			_usage = usage ?? throw new ArgumentNullException(nameof(usage));

			Cluster = cluster;
			Responses = usage.CallOnce(ClientUsage);
			UniqueValues = usage.CallUniqueValues;
		}

		public virtual ElasticsearchClient Client =>
			TestConfiguration.Instance.RunIntegrationTests ? Cluster.Client : TestClient.DefaultInMemoryClient;

		public TCluster Cluster { get; }

		protected virtual string CallIsolatedValue => UniqueValues.Value;
		protected virtual Action<TDescriptor> Fluent { get; } = null;
		protected virtual TInitializer Initializer { get; } = null;
		protected bool RanIntegrationSetup => _usage?.CalledSetup ?? false;
		protected LazyResponses Responses { get; }

		protected virtual bool TestOnlyOne => TestClient.Configuration.TestOnlyOne;

		protected CallUniqueValues UniqueValues { get; }

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected string U(string s) => Uri.EscapeDataString(s);

		protected string Q(string s) => Uri.EscapeDataString(s);

		protected T ExtendedValue<T>(string key) where T : class => UniqueValues.ExtendedValue<T>(key);

		protected bool TryGetExtendedValue<T>(string key, out T t) where T : class => UniqueValues.TryGetExtendedValue(key, out t);

		protected void ExtendedValue<T>(string key, T value) where T : class => UniqueValues.ExtendedValue(key, value);
		
		protected virtual void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values) { }

		protected virtual void IntegrationTeardown(ElasticsearchClient client, CallUniqueValues values) { }

		protected virtual void OnBeforeCall(ElasticsearchClient client) { }

		protected virtual void OnAfterCall(ElasticsearchClient client) { }

		protected virtual TDescriptor NewDescriptor() => Activator.CreateInstance<TDescriptor>();

		protected abstract LazyResponses ClientUsage();

		protected LazyResponses Calls(
			Func<ElasticsearchClient, Action<TDescriptor>, TResponse> fluent,
			Func<ElasticsearchClient, Action<TDescriptor>, Task<TResponse>> fluentAsync,
			Func<ElasticsearchClient, TInitializer, TResponse> request,
			Func<ElasticsearchClient, TInitializer, Task<TResponse>> requestAsync
		) => new(async () =>
		{
			var client = Client;

			void IntegrateOnly(Action<ElasticsearchClient> act)
			{
				if (!TestClient.Configuration.RunIntegrationTests) return;

				act(client);
			}

			if (TestClient.Configuration.RunIntegrationTests)
			{
				IntegrationSetup(client, UniqueValues);
				_usage.CalledSetup = true;
			}

			static (ClientMethod, Func<ValueTask<TResponse>>) Api(ClientMethod method, Func<ValueTask<TResponse>> action) => (method, action);

			var dict = new Dictionary<ClientMethod, ElasticsearchResponse>();
			var views = new[]
			{
				Api(ClientMethod.Initializer, () => new ValueTask<TResponse>(request(client, Initializer))),
				Api(ClientMethod.InitializerAsync, async () => await requestAsync(client, Initializer)),
				Api(ClientMethod.Fluent, () => new ValueTask<TResponse>(fluent(client, Fluent))),
				Api(ClientMethod.FluentAsync, async () => await fluentAsync(client, Fluent)),
			};

			foreach (var (clientMethod, m) in views.OrderBy(_ => Gimme.Random.Int()))
			{
				UniqueValues.CurrentView = clientMethod;
				IntegrateOnly(OnBeforeCall);
				dict.Add(clientMethod, await m());
				IntegrateOnly(OnAfterCall);
				if (TestOnlyOne) break;
			}

			if (!TestClient.Configuration.RunIntegrationTests) return dict;

			IntegrationTeardown(client, UniqueValues);
			_usage.CalledTeardown = true;

			return dict;
		});

		protected virtual async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await Responses;
			foreach (var (key, value) in responses)
			{
				var response = value as TResponse;
				try
				{
					UniqueValues.CurrentView = key;
					assert(response);
				}
#pragma warning disable 7095 //enable this if you expect a single overload to act up
#pragma warning disable 8360
				catch (Exception ex) when (false)
#pragma warning restore 7095
#pragma warning restore 8360
#pragma warning disable 0162 //dead code while the previous exception filter is false
				{
					throw new Exception($"asserting over the response from: {key} failed: {ex.Message}", ex);
				}
#pragma warning restore 0162
			}
		}

		protected virtual async Task AssertOnAllResponses(Func<TResponse, Task> assert)
		{
			var responses = await Responses;
			foreach (var (key, value) in responses)
			{
				var response = value as TResponse;
				try
				{
					UniqueValues.CurrentView = key;
					await assert(response);
				}
#pragma warning disable 7095 //enable this if you expect a single overload to act up
#pragma warning disable 8360
				catch (Exception ex) when (false)
#pragma warning restore 7095
#pragma warning restore 8360
#pragma warning disable 0162 //dead code while the previous exception filter is false
				{
					throw new Exception($"asserting over the response from: {key} failed: {ex.Message}", ex);
				}
#pragma warning restore 0162
			}
		}
	}
}
